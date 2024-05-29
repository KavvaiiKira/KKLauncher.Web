using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Contracts.Apps;
using Microsoft.AspNetCore.Components;

namespace KKLauncher.Web.Client.Pages.AppComponents
{
    public partial class AppPageComponent
    {
        [Parameter]
        public AppViewDto? App { get; set; }

        private string _appImageUrl = string.Empty;

        protected override void OnParametersSet()
        {
            if (App == null || App.Image.Length == 0)
            {
                return;
            }

            var imagesrc = Convert.ToBase64String(App.Image);
            _appImageUrl = string.Format("data:image/png;base64,{0}", imagesrc);
        }

        private async Task StartAsync()
        {
            if (App == null)
            {
                return;
            }

            var startResult = await _appStartService.StartAppAsync(App.Id);
            if (!startResult.Success)
            {
                //TODO: Toasts
            }
        }

        private async Task StartWithSteam()
        {
            if (App == null || !App.IsSteamStartAvailable)
            {
                return;
            }
        }

        private async Task Edit()
        {

        }

        private async Task DeleteAsync()
        {
            if (App != null)
            {
                await _bus.Publish(new AppDeletedEvent(App.Id));
            }
        }
    }
}
