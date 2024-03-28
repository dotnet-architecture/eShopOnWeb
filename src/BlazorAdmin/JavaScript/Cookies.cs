using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorAdmin.JavaScript;

public class Cookies
{
    private readonly IJSRuntime _jsRuntime;

    public Cookies(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task DeleteCookieAsync(string name)
    {
        await _jsRuntime.InvokeAsync<string>(JSInteropConstants.DeleteCookie, name);
    }

    public async Task<string> GetCookieAsync(string name)
    {
        return await _jsRuntime.InvokeAsync<string>(JSInteropConstants.GetCookie, name);
    }
}
