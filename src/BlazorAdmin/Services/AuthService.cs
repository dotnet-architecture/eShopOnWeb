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

        public string ApiUrl => Constants.GetApiUrl(InDocker);
        public string WebUrl => Constants.GetWebUrl(InDocker);

        private static bool InDocker { get; set; }

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
            return await _httpClient.GetAsync($"{ApiUrl}{uri}");
        }

        public async Task<HttpResponseMessage> HttpDelete(string uri, int id)
        {
            return await _httpClient.DeleteAsync($"{ApiUrl}{uri}/{id}");
        }

        public async Task<HttpResponseMessage> HttpPost(string uri, object dataToSend)
        {
            var content = ToJson(dataToSend);

            return await _httpClient.PostAsync($"{ApiUrl}{uri}", content);
        }

        public async Task<HttpResponseMessage> HttpPut(string uri, object dataToSend)
        {
            var content = ToJson(dataToSend);

            return await _httpClient.PutAsync($"{ApiUrl}{uri}", content);
        }

        public async Task Logout()
        {
            await DeleteLocalStorage();
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

            var inDocker = await new Cookies(_jSRuntime).GetCookie("inDocker");
            await SaveInDockerInLocalStorage(inDocker);

            await RefreshLoginInfo();
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

        public async Task<bool> GetInDocker()
        {
            return (await _localStorage.GetItemAsync<string>("inDocker")).ToLower() == "true";
        }

        private StringContent ToJson(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        private async Task LogoutIdentityManager()
        {
            await _httpClient.PostAsync("Identity/Account/Logout", null);
        }

        private async Task DeleteLocalStorage()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("username");
            await _localStorage.RemoveItemAsync("inDocker");
        }

        private async Task DeleteCookies()
        {
            await new Cookies(_jSRuntime).DeleteCookie("token");
            await new Cookies(_jSRuntime).DeleteCookie("username");
            await new Cookies(_jSRuntime).DeleteCookie("inDocker");
        }

        private async Task SetLoginData()
        {
            IsLoggedIn = !string.IsNullOrEmpty(await GetToken());
            UserName = await GetUsername();
            InDocker = await GetInDocker();
            await SetAuthorizationHeader();
        }

        private void RemoveAuthorizationHeader()
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
            }
        }

        private async Task SaveTokenInLocalStorage(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return;
            }
            await _localStorage.SetItemAsync("authToken", token);
        }

        private async Task SaveUsernameInLocalStorage(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return;
            }
            await _localStorage.SetItemAsync("username", username);
        }

        private async Task SaveInDockerInLocalStorage(string inDocker)
        {
            if (string.IsNullOrEmpty(inDocker))
            {
                return;
            }
            await _localStorage.SetItemAsync("inDocker", inDocker);
        }

        private async Task SetAuthorizationHeader()
        {
            var token = await GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

    }
}
