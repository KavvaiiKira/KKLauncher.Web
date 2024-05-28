using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Contracts.PCs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace KKLauncher.Web.Client.Pages.Main
{
    public partial class LoginComponent
    {
        private string _ip = string.Empty;
        private string _password = string.Empty;
        private MarkupString _errors = new MarkupString();

        protected override async Task OnInitializedAsync()
        {
            var loginToken = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (loginToken == null || string.IsNullOrEmpty(loginToken.LoginIp))
            {
                return;
            }

            _ip = loginToken.LoginIp;
        }

        private void OnIpInput(ChangeEventArgs args)
        {
            _ip = args.Value?.ToString() ?? string.Empty;
        }

        private void OnPasswordInput(ChangeEventArgs args)
        {
            _password = args.Value?.ToString() ?? string.Empty;
        }

        private async Task OnKeyDown(KeyboardEventArgs e)
        {
            if (e.Code != "Enter" && e.Code != "NumpadEnter")
            {
                return;
            }

            _errors = new MarkupString();
            var errors = string.Empty;

            if (string.IsNullOrEmpty(_ip))
            {
                errors += "· IP required!<br />";
            }

            if (string.IsNullOrEmpty(_password))
            {
                errors += "· Password required!<br />";
            }

            if (!string.IsNullOrEmpty(errors))
            {
                _errors = (MarkupString)errors;

                StateHasChanged();

                return;
            }

            var loginResult = await _loginService.LoginPcAsync(new PCDto() { LocalIp = _ip, Password = _password });
            if (loginResult.Success)
            {
                await _localStorageService.SetItemAsync(
                    nameof(LoginToken),
                    new LoginToken()
                    {
                        LoginIp = _ip,
                        LoggedIn = true
                    });

                _navigationManager.NavigateTo("/", true);

                return;
            }

            _errors = (MarkupString)$"· {loginResult.Message}";

            StateHasChanged();
        }
    }
}
