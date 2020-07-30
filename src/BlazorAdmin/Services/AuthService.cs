using System.Net.Http;
using System.Threading.Tasks;
using BlazorAdmin.JavaScript;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using BlazorShared.Authorization;

namespace BlazorAdmin.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jSRuntime;
        private static bool InDocker { get; set; }

        public string ApiUrl => Constants.GetApiUrl(InDocker);
        public bool IsLoggedIn { get; set; }

        public AuthService(HttpClient httpClient,
            ILocalStorageService localStorage,
            IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _jSRuntime = jSRuntime;
        }

        public async Task Logout()
        {
            await DeleteLocalStorage();
            await DeleteCookies();
            IsLoggedIn = false;
            await LogoutIdentityManager();
        }

        public async Task RefreshLoginInfo()
        {
            await SetLoginData();
        }

        public async Task RefreshLoginInfoFromCookie()
        {
            var inDocker = await new Cookies(_jSRuntime).GetCookie("inDocker");
            await SaveInDockerInLocalStorage(inDocker);

            await RefreshLoginInfo();
        }

        public async Task<bool> GetInDocker()
        {
            return (await _localStorage.GetItemAsync<string>("inDocker")).ToLower() == "true";
        }

        private async Task LogoutIdentityManager()
        {
            await _httpClient.PostAsync("Identity/Account/Logout", null);
        }

        private async Task DeleteLocalStorage()
        {
            await _localStorage.RemoveItemAsync("inDocker");
        }

        private async Task DeleteCookies()
        {
            await new Cookies(_jSRuntime).DeleteCookie("token");
            await new Cookies(_jSRuntime).DeleteCookie("inDocker");
        }

        private async Task SetLoginData()
        {
            // TODO: See if we can eliminate docker logic outside of Program.cs, which can pull the right config as required
            InDocker = await GetInDocker();
        }

        private async Task SaveInDockerInLocalStorage(string inDocker)
        {
            if (string.IsNullOrEmpty(inDocker))
            {
                return;
            }
            await _localStorage.SetItemAsync("inDocker", inDocker);
        }
    }
}
