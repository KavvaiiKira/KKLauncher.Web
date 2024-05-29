using BlazorComponentBus;
using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Contracts.Collections;

namespace KKLauncher.Web.Client.Pages.NavigationComponents
{
    public partial class CollectionsMenu
    {
        private List<CollectionViewDto> _collections = new List<CollectionViewDto>();

        private bool _addCollectionShown = false;

        protected override async Task OnInitializedAsync()
        {
            var token = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (token == null)
            {
                return;
            }

            var collections = await _collectionService.GetCollectionsByPCLocalIpAsync(token.LoginIp);
            _collections = collections.ToList();
        }

        protected override void OnParametersSet()
        {
            _bus.Subscribe<CloseAddCollectionFormEvent>(CloseAddCollectionFormEventHandler);
            _bus.Subscribe<CollectionAddedEvent>(CollectionAddedEventHandler);
        }

        private void ShowAddCollectionForm()
        {
            if (_addCollectionShown)
            {
                return;
            }

            _addCollectionShown = true;
            StateHasChanged();
        }

        private void CloseAddCollectionFormEventHandler(MessageArgs message)
        {
            if (!_addCollectionShown)
            {
                return;
            }

            _addCollectionShown = false;
            StateHasChanged();
        }

        private async Task CollectionAddedEventHandler(MessageArgs message, CancellationToken cancellationToken)
        {
            var eventItem = message.GetMessage<CollectionAddedEvent>();
            if (eventItem == null)
            {
                return;
            }

            var newCollection = await _collectionService.GetCollectionViewByIdAsync(eventItem.NewCollectionId);
            if (newCollection == null)
            {
                //TODO: Toasts
                return;
            }

            if (_collections.Any(c => c.Id == newCollection.Id))
            {
                return;
            }

            _collections.Add(newCollection);

            StateHasChanged();
        }

        public void Dispose()
        {
            _bus.UnSubscribe<CloseAddCollectionFormEvent>(CloseAddCollectionFormEventHandler);
            _bus.UnSubscribe<CollectionAddedEvent>(CollectionAddedEventHandler);
        }
    }
}
