using System.Threading.Tasks;
using BlazorAdmin.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAdmin.Helper
{
    public class BlazorComponent : ComponentBase
    {
        [Inject] private RefreshService Refresh { get; set; }

        protected override Task OnInitializedAsync()
        {
            Refresh.RefreshRequested += DoRefresh;
            return base.OnInitializedAsync();
        }

        public void CallRequestRefresh()
        {
            Refresh.CallRequestRefresh();
        }

        private void DoRefresh()
        {
            StateHasChanged();
        }

    }
}
