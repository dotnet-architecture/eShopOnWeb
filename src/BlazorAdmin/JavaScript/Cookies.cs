using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorAdmin.JavaScript
{
    public class Cookies
    {
        private readonly IJSRuntime _jsRuntime;

        public Cookies(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task DeleteCookie(string name)
        {
            await _jsRuntime.InvokeAsync<string>(JSInteropConstants.DeleteCookie, name);
        }

        public async Task<string> GetCookie(string name)
        {
            return await _jsRuntime.InvokeAsync<string>(JSInteropConstants.GetCookie, name);
        }
    }
}
