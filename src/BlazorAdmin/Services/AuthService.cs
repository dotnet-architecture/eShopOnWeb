using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Newtonsoft.Json;

namespace BlazorAdmin.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public HttpClient GetHttpClient()
        {
            return _httpClient;
        }

        public async Task Login(AuthRequest user)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{Constants.API_URL}authenticate", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                await SaveToken(response);
                await SaveUsername(response);
                await SetAuthorizationHeader();

                UserName = await GetUsername();
                IsLoggedIn = true;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("username");
            RemoveAuthorizationHeader();
            UserName = null;
            IsLoggedIn = false;
        }

        public async Task RefreshLoginInfo()
        {
            IsLoggedIn = !string.IsNullOrEmpty(await GetToken());
            UserName = await GetUsername();
            await SetAuthorizationHeader();
        }

        private async Task SaveToken(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

            await _localStorage.SetItemAsync("authToken", jwt.Token);
        }

        private void RemoveAuthorizationHeader()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        private async Task SaveUsername(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<AuthResponse>(responseContent);

            await _localStorage.SetItemAsync("username", jwt.Username);
        }

        public async Task<string> GetToken()
        {

            var token = await _localStorage.GetItemAsync<string>("authToken");
            return token;
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

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
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
