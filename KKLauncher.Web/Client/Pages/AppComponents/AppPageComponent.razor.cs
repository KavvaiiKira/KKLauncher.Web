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
            _appImageUrl = string.Format("data:image/jpeg;base64,{0}", imagesrc);
        }

        private async Task Start()
        {

        }

        private async Task StartWithSteam()
        {

        }

        private async Task Edit()
        {

        }

        private async Task Delete()
        {

        }
    }
}
