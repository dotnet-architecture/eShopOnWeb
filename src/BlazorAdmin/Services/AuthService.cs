using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
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
        private readonly string _apiUrl = Constants.GetApiUrl();

        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }

        public AuthService(HttpClient httpClient, ILocalStorageService localStorage, IJSRuntime jSRuntime)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _jSRuntime = jSRuntime;
        }

        public async Task<HttpResponseMessage> HttpGet(string uri)
        {
            return await _httpClient.GetAsync($"{_apiUrl}{uri}");
        }

        public async Task<HttpResponseMessage> HttpDelete(string uri, int id)
        {
            return await _httpClient.DeleteAsync($"{_apiUrl}{uri}/{id}");
        }

        public async Task<HttpResponseMessage> HttpPost(string uri, object dataToSend)
        {
            var content = ToJson(dataToSend);

            return await _httpClient.PostAsync($"{_apiUrl}{uri}", content);
        }

        public async Task<HttpResponseMessage> HttpPut(string uri, object dataToSend)
        {
            var content = ToJson(dataToSend);

            return await _httpClient.PutAsync($"{_apiUrl}{uri}", content);
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

        private StringContent ToJson(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
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

    }
}
