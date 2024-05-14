using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Client.Forms.ImageSelectForms;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.RegularExpressions;
using KKLauncher.Web.Client.Constants;
using KKLauncher.Web.Client.Models;

namespace KKLauncher.Web.Client.Forms
{
    public partial class AddAppForm
    {
        private AppDto? _app { get; set; }
        private EditContext? _editContext;
        private ValidationMessageStore? _messageStore;
        private bool _show = true;
        private AppImageSelectForm? _appImageSelectForm;

        protected override void OnInitialized()
        {
            _app ??= new AppDto();
            _editContext = new EditContext(_app);
            _editContext.OnValidationRequested += HandleValidationRequested;
            _messageStore = new ValidationMessageStore(_editContext);
        }

        private void HandleValidationRequested(object? sender,
            ValidationRequestedEventArgs args)
        {
            _messageStore?.Clear();

            if (string.IsNullOrWhiteSpace(_app!.Name))
            {
                _messageStore?.Add(() => _app.Name, "Application name must not be EMPTY!");
            }
            else if (_app!.Name.Length > 20)
            {
                _messageStore?.Add(() => _app.Name, "Application name must be shorter than 20 characters!");
            }

            if (string.IsNullOrEmpty(_app!.Path))
            {
                _messageStore?.Add(() => _app.Path, "Application path must not be EMPTY!");
            }
            else if (!Regex.IsMatch(_app!.Path, RegexConstants.PathRegex))
            {
                _messageStore?.Add(() => _app.Path, "Invalid application path format!");
            }

            if (!string.IsNullOrEmpty(_app!.SteamId) && !Regex.IsMatch(_app!.SteamId, @"^\d+$"))
            {
                _messageStore?.Add(() => _app.SteamId, "SteamID can only contain numbers!");
            }
        }

        private async Task ValidSubmit()
        {
            if (_app == null)
            {
                return;
            }

            var loginToken = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (loginToken == null)
            {
                //TODO: Toasts
                return;
            }

            _app.Id = Guid.NewGuid();
            _app.Image = await _appImageSelectForm!.GetImage();
            _app.PCLocalIp = loginToken.LoginIp;

            //TODO: Toasts
            await _appService.AddAppAsync(_app);
        }

        private async Task Cancel()
        {
            await _bus.Publish(new CloseAddAppFormEvent());
        }

        public void Dispose()
        {
            if (_editContext is not null)
            {
                _editContext.OnValidationRequested -= HandleValidationRequested;
            }
        }
    }
}
