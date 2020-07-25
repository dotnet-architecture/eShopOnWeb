using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlazorAdmin.JavaScript;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using BlazorShared.Authorization;

namespace BlazorAdmin.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jSRuntime;
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _jSRuntime = jSRuntime;
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        public async Task<AuthResponse> LoginWithoutSaveToLocalStorage(AuthRequest user)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Constants.API_URL}authenticate", jsonContent);
            var authResponse = new AuthResponse();

            if (response.IsSuccessStatusCode)
            {
                authResponse = await DeserializeToAuthResponse(response);

                IsLoggedIn = true;
            }

            return authResponse;
        }

        public async Task<AuthResponse> Login(AuthRequest user)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Constants.API_URL}authenticate", jsonContent);
            var authResponse = new AuthResponse();

            if (response.IsSuccessStatusCode)
            {
                authResponse = await DeserializeToAuthResponse(response);
                await SaveTokenInLocalStorage(authResponse);
                await SaveUsernameInLocalStorage(authResponse);
                await SetAuthorizationHeader();

                UserName = await GetUsername();
                IsLoggedIn = true;
            }

            return authResponse;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("username");
            await DeleteCookies();
            RemoveAuthorizationHeader();
            UserName = null;
            IsLoggedIn = false;
            await LogoutIdentityManager();
        }

        public async Task RefreshLoginInfo()
        {
            await SetLoginData();
        }

        public async Task RefreshLoginInfoFromCookie()
        {
            var token = await new Cookies(_jSRuntime).GetCookie("token");
            await SaveTokenInLocalStorage(token);

            var username = await new Cookies(_jSRuntime).GetCookie("username");
            await SaveUsernameInLocalStorage(username);

            await RefreshLoginInfo();
        }
        private async Task LogoutIdentityManager()
        {
            await _httpClient.PostAsync("Identity/Account/Logout", null);
        }

        private async Task DeleteCookies()
        {
            await new Cookies(_jSRuntime).DeleteCookie("token");
            await new Cookies(_jSRuntime).DeleteCookie("username");
        }

        private async Task SetLoginData()
        {
            IsLoggedIn = !string.IsNullOrEmpty(await GetToken());
            UserName = await GetUsername();
            await SetAuthorizationHeader();
        }

        private async Task<AuthResponse> DeserializeToAuthResponse(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AuthResponse>(responseContent);
        }

        private async Task SaveTokenInLocalStorage(AuthResponse authResponse)
        {
            await _localStorage.SetItemAsync("authToken", SaveTokenInLocalStorage(authResponse.Token));
        }

        private async Task SaveTokenInLocalStorage(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
            await _localStorage.SetItemAsync("authToken", token);
        }

        private void RemoveAuthorizationHeader()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        private async Task SaveUsernameInLocalStorage(AuthResponse authResponse)
        {
            await _localStorage.SetItemAsync("username", SaveUsernameInLocalStorage(authResponse.Username));
        }

        private async Task SaveUsernameInLocalStorage(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return;
            }
            await _localStorage.SetItemAsync("username", username);
        }

        public async Task<string> GetToken()
        {

            var token = await _localStorage.GetItemAsync<string>("authToken");
            return token;
        }

        public async Task<UserInfo> GetTokenFromController()
        {
            return await _httpClient.GetFromJsonAsync<UserInfo>("User");
        }

        public async Task<string> GetUsername()
        {
            var username = await _localStorage.GetItemAsync<string>("username");
            return username;
        }

        private async Task SetAuthorizationHeader()
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            if (string.IsNullOrEmpty(jwt))
            {
                return claims;
            }

            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, object>>(Encoding.UTF8.GetString(jsonBytes));

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonConvert.DeserializeObject<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
