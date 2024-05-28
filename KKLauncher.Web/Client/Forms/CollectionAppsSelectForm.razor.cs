using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Contracts.Apps;
using Microsoft.AspNetCore.Components;

namespace KKLauncher.Web.Client.Forms
{
    public partial class CollectionAppsSelectForm
    {
        private IEnumerable<AppViewDto> _searchedApps = Enumerable.Empty<AppViewDto>();
        private IEnumerable<AppViewDto> _pinnedApps = Enumerable.Empty<AppViewDto>();

        private string _localIp = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var token = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (token == null)
            {
                return;
            }

            _localIp = token.LoginIp;
        }

        private async Task SearchAppNameOnInput(ChangeEventArgs args)
        {
            if (string.IsNullOrEmpty(_localIp))
            {
                return;
            }

            var searchAppNameKey = args.Value?.ToString();
            if (string.IsNullOrEmpty(searchAppNameKey))
            {
                return;
            }

            _searchedApps = await _appService.SearchAppsAsync(_localIp, searchAppNameKey);
        }

        public List<Guid> GetPinnedAppIds()
        {
            return _pinnedApps.Select(a => a.Id).ToList();
        }
    }
}
