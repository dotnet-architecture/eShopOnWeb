using Azure.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Azure.Identity
{
    public class AzureDeveloperCliCredential : TokenCredential
    {
        internal const string AzdCliNotInstalled = $"Azure Developer CLI could not be found. {Troubleshoot}";
        internal const string AzdNotLogIn = "Please run 'azd login' from a command prompt to authenticate before using this credential.";
        internal const string WinAzdCliError = "'azd is not recognized";
        internal const string AzdCliTimeoutError = "Azure Developer CLI authentication timed out.";
        internal const string AzdCliFailedError = "Azure Developer CLI authentication failed due to an unknown error.";
        internal const string Troubleshoot = "Please visit https://aka.ms/azure-dev for installation instructions and then, once installed, authenticate to your Azure account using 'azd login'.";
        internal const string InteractiveLoginRequired = "Azure Developer CLI could not login. Interactive login is required.";
        private const string RefreshTokeExpired = "The provided authorization code or refresh token has expired due to inactivity. Send a new interactive authorization request for this user and resource.";

        private static readonly string DefaultWorkingDirWindows = Environment.GetFolderPath(Environment.SpecialFolder.System);
        private const string DefaultWorkingDirNonWindows = "/bin/";
        private static readonly string DefaultWorkingDir = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? DefaultWorkingDirWindows : DefaultWorkingDirNonWindows;

        private static readonly Regex AzdNotFoundPattern = new Regex("azd:(.*)not found");

        public override AccessToken GetToken(TokenRequestContext requestContext, CancellationToken cancellationToken = default)
        {
            return RequestCliAccessTokenAsync(requestContext, cancellationToken)
                .GetAwaiter()
                .GetResult();
        }

        public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, CancellationToken cancellationToken = default)
        {
            return await RequestCliAccessTokenAsync(requestContext, cancellationToken).ConfigureAwait(false);
        }

        private async ValueTask<AccessToken> RequestCliAccessTokenAsync(TokenRequestContext context, CancellationToken cancellationToken)
        {
            try
            {
                ProcessStartInfo processStartInfo = GetAzdCliProcessStartInfo(context.Scopes);
                string output = await RunProcessAsync(processStartInfo);

                return DeserializeOutput(output);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                throw new AuthenticationFailedException(AzdCliTimeoutError);
            }
            catch (InvalidOperationException exception)
            {
                bool isWinError = exception.Message.StartsWith(WinAzdCliError, StringComparison.CurrentCultureIgnoreCase);
                bool isOtherOsError = AzdNotFoundPattern.IsMatch(exception.Message);

                if (isWinError || isOtherOsError)
                {
                    throw new CredentialUnavailableException(AzdCliNotInstalled);
                }

                bool isAADSTSError = exception.Message.Contains("AADSTS");
                bool isLoginError = exception.Message.IndexOf("azd login", StringComparison.OrdinalIgnoreCase) != -1;

                if (isLoginError && !isAADSTSError)
                {
                    throw new CredentialUnavailableException(AzdNotLogIn);
                }

                bool isRefreshTokenFailedError = exception.Message.IndexOf(AzdCliFailedError, StringComparison.OrdinalIgnoreCase) != -1 &&
                                                 exception.Message.IndexOf(RefreshTokeExpired, StringComparison.OrdinalIgnoreCase) != -1 ||
                                                 exception.Message.IndexOf("CLIInternalError", StringComparison.OrdinalIgnoreCase) != -1;

                if (isRefreshTokenFailedError)
                {
                    throw new CredentialUnavailableException(InteractiveLoginRequired);
                }

                throw new AuthenticationFailedException($"{AzdCliFailedError} {Troubleshoot} {exception.Message}");
            }
            catch (Exception ex)
            {
                throw new CredentialUnavailableException($"{AzdCliFailedError} {Troubleshoot} {ex.Message}");
            }
        }

        private async ValueTask<string> RunProcessAsync(ProcessStartInfo processStartInfo, CancellationToken cancellationToken = default)
        {
            var process = Process.Start(processStartInfo);
            if (process == null)
            {
                throw new CredentialUnavailableException(AzdCliFailedError);
            }

            await process.WaitForExitAsync(cancellationToken);

            if (process.ExitCode != 0)
            {
                var errorMessage = process.StandardError.ReadToEnd();
                throw new InvalidOperationException(errorMessage);
            }

            return process.StandardOutput.ReadToEnd();
        }

        private ProcessStartInfo GetAzdCliProcessStartInfo(string[] scopes)
        {
            string scopeArgs = string.Join(" ", scopes.Select(scope => string.Format($"--scope {scope}")));
            string command = $"azd auth token --output json {scopeArgs}";

            string fileName;
            string argument;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
                argument = $"/c \"{command}\"";
            }
            else
            {
                fileName = "/bin/sh";
                argument = $"-c \"{command}\"";
            }

            return new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = argument,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                ErrorDialog = false,
                CreateNoWindow = true,
                WorkingDirectory = DefaultWorkingDir,
            };
        }

        private static AccessToken DeserializeOutput(string output)
        {
            using JsonDocument document = JsonDocument.Parse(output);

            JsonElement root = document.RootElement;
            string accessToken = root.GetProperty("token").GetString();
            DateTimeOffset expiresOn = root.GetProperty("expiresOn").GetDateTimeOffset();

            return new AccessToken(accessToken, expiresOn);
        }
    }
}
