using BlazorComponentBus;
using KKLauncher.Web.Client.Events;

namespace KKLauncher.Web.Client.Pages.NavigationComponents
{
    public partial class CollectionsMenu
    {
        private bool _addCollectionShown = false;

        protected override void OnParametersSet()
        {
            _bus.Subscribe<CloseAddCollectionFormEvent>(CloseAddCollectionFormEventHandler);
        }

        private async Task ShowAddCollectionForm()
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

        public void Dispose()
        {
            _bus.UnSubscribe<CloseAddCollectionFormEvent>(CloseAddCollectionFormEventHandler);
        }
    }
}
