using System.Net.Http;
using System.Threading.Tasks;
using BlazorAdmin.JavaScript;
using Blazored.LocalStorage;
using Microsoft.JSInterop;

namespace BlazorAdmin.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jSRuntime;
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
            await DeleteCookies();
            IsLoggedIn = false;
            await LogoutIdentityManager();
        }

        private async Task LogoutIdentityManager()
        {
            await _httpClient.PostAsync("Identity/Account/Logout", null);
        }

        private async Task DeleteCookies()
        {
            await new Cookies(_jSRuntime).DeleteCookie("token");
        }
    }
}
