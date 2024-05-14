using KKLauncher.Web.Client.Models;

namespace KKLauncher.Web.Client.Pages.NavigationComponents
{
    public partial class NavigationFooter
    {
        private bool _scenarioFormShown = false;
        private bool _settingsFormShown = false;

        private void ShowScenarionForm()
        {
            if (!_scenarioFormShown)
            {
                _settingsFormShown = false;
                _scenarioFormShown = true;
                StateHasChanged();
            }
        }

        private void ShowSettingsForm()
        {
            if (!_settingsFormShown)
            {
                _scenarioFormShown = false;
                _settingsFormShown = true;
                StateHasChanged();
            }
        }

        private async Task LogOut()
        {
            var token = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (token == null || !token.LoggedIn)
            {
                _navigationManager.NavigateTo("/", true);

                return;
            }

            token.LoggedIn = false;

            await _localStorageService.SetItemAsync(nameof(LoginToken), token);

            _navigationManager.NavigateTo("/", true);
        }
    }
}
