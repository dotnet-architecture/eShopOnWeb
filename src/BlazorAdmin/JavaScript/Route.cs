using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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
            await _jsRuntime.InvokeAsync<string>("routeOutside", path);
        }
    }
}
