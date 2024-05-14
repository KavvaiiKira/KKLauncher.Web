using BlazorComponentBus;
using KKLauncher.Web.Client.Events;

namespace KKLauncher.Web.Client.Pages.AppComponents
{
    public partial class AddAppComponent
    {
        private bool _addAppShown = false;

        protected override void OnParametersSet()
        {
            _bus.Subscribe<CloseAddAppFormEvent>(CloseAddAppFormEventHandler);
        }

        private void ShowAddAppForm()
        {
            if (_addAppShown)
            {
                return;
            }

            _addAppShown = true;
            StateHasChanged();
        }

        private void CloseAddAppFormEventHandler(MessageArgs message)
        {
            if (!_addAppShown)
            {
                return;
            }

            var eventItem = message.GetMessage<CloseAddAppFormEvent>();
            if (eventItem == null)
            {
                return;
            }

            _addAppShown = false;
            StateHasChanged();
        }

        public void Dispose()
        {
            _bus.UnSubscribe<CloseAddAppFormEvent>(CloseAddAppFormEventHandler);
        }
    }
}
