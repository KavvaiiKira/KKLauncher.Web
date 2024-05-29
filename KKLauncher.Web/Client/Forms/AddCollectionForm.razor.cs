using Blazored.LocalStorage;
using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Contracts.Collections;
using Microsoft.AspNetCore.Components.Forms;

namespace KKLauncher.Web.Client.Forms
{
    public partial class AddCollectionForm
    {
        private CollectionDto? _collection { get; set; }
        private EditContext? _editContext;
        private ValidationMessageStore? _messageStore;
        private bool _show = true;
        private CollectionAppsSelectForm? _collectionAppsSelectForm;

        protected override void OnInitialized()
        {
            _collection ??= new CollectionDto();
            _editContext = new EditContext(_collection);
            _editContext.OnValidationRequested += HandleValidationRequested;
            _messageStore = new ValidationMessageStore(_editContext);
        }

        private void HandleValidationRequested(object? sender,
            ValidationRequestedEventArgs args)
        {
            _messageStore?.Clear();

            if (string.IsNullOrWhiteSpace(_collection!.Name))
            {
                _messageStore?.Add(() => _collection.Name, "Collection name must not be EMPTY!");
            }
            else if (_collection!.Name.Length > 20)
            {
                _messageStore?.Add(() => _collection.Name, "Collection name must be shorter than 20 characters!");
            }
        }

        private async Task ValidSubmit()
        {
            if (_collection == null)
            {
                return;
            }

            var loginToken = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (loginToken == null)
            {
                //TODO: Toasts
                return;
            }

            _collection.Id = Guid.NewGuid();
            _collection.AppIds = _collectionAppsSelectForm!.GetPinnedAppIds();
            _collection.PCLocalIp = loginToken.LoginIp;

            if (!await _collectionService.AddCollectionAsync(_collection))
            {
                //TODO: Toasts
                return;
            }

            await Cancel();

            await _bus.Publish(new CollectionAddedEvent(_collection.Id));
        }

        private async Task Cancel()
        {
            await _bus.Publish(new CloseAddCollectionFormEvent());
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
