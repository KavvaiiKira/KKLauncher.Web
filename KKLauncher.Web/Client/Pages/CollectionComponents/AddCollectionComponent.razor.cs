using BlazorComponentBus;
using KKLauncher.Web.Client.Events;

namespace KKLauncher.Web.Client.Pages.CollectionComponents
{
    public partial class AddCollectionComponent
    {
        private bool _addCollectionShown = false;

        protected override void OnParametersSet()
        {
            _bus.Subscribe<CloseAddCollectionFormEvent>(CloseAddCollectionFormEventHandler);
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

            var eventItem = message.GetMessage<CloseAddCollectionFormEvent>();
            if (eventItem == null)
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
