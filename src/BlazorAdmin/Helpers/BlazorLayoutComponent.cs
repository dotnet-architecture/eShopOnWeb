using Microsoft.AspNetCore.Components;

namespace BlazorAdmin.Helpers
{
    public class BlazorLayoutComponent : LayoutComponentBase
    {
        private readonly RefreshBroadcast _refresh = RefreshBroadcast.Instance;

        protected override void OnInitialized()
        {
            _refresh.RefreshRequested += DoRefresh;
            base.OnInitialized();
        }

        public void CallRequestRefresh()
        {
            _refresh.CallRequestRefresh();
        }

        private void DoRefresh()
        {
            StateHasChanged();
        }
    }
}
