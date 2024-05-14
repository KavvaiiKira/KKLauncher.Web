using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Forms.ImageSelectForms;
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
        private CollectionImageSelectForm? _collectionImageSelectForm;

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

            _collection.Id = Guid.NewGuid();
            _collection.Image = await _collectionImageSelectForm!.GetImage();
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
