using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorAdmin.JavaScript
{
    public class Route
    {
        private readonly IJSRuntime _jsRuntime;

        public Route(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task RouteOutside(string path)
        {
            await _jsRuntime.InvokeAsync<string>(JSInteropConstants.RouteOutside, path);
        }
    }
}
