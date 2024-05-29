using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Contracts.Apps;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace KKLauncher.Web.Client.Forms
{
    public partial class CollectionAppsSelectForm
    {
        private IEnumerable<AppViewDto> _searchedApps = Enumerable.Empty<AppViewDto>();
        private List<AppViewDto> _pinnedApps = new List<AppViewDto>();
        private List<Guid> _pinnedAppIds = new List<Guid>();

        private string _localIp = string.Empty;

        private bool _appsFound = true;

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
                if (_searchedApps.Any())
                {
                    _searchedApps = Enumerable.Empty<AppViewDto>();
                }

                _appsFound = true;

                StateHasChanged();

                return;
            }

            _searchedApps = await _appService.SearchAppsAsync(_localIp, searchAppNameKey);
            _appsFound = _searchedApps.Any();

            StateHasChanged();
        }

        private void SearchedAppDoubleClick(MouseEventArgs args, Guid appId)
        {
            if (_pinnedAppIds.Contains(appId))
            {
                return;
            }

            var searchedApp = _searchedApps.FirstOrDefault(a => a.Id == appId);
            if (searchedApp == null)
            {
                return;
            }

            _pinnedApps.Add(searchedApp);
            _pinnedAppIds.Add(appId);

            StateHasChanged();
        }

        private void RemoveAppFromPinned(MouseEventArgs args, Guid appId)
        {
            if (!_pinnedAppIds.Contains(appId))
            {
                return;
            }

            _pinnedAppIds.Remove(appId);
            _pinnedApps.RemoveAll(a => a.Id == appId);

            StateHasChanged();
        }

        public List<Guid> GetPinnedAppIds()
        {
            return _pinnedAppIds;
        }
    }
}
