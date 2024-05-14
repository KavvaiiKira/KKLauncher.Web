using BlazorComponentBus;
using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Models;

namespace KKLauncher.Web.Client.Pages.Main
{
    public partial class MainComponent
    {
        private string _centerHeader = string.Empty;
        private Type? _selectedComponentType = null;
        private Dictionary<string, object>? _selectedComponentParameters = null;

        protected override void OnParametersSet()
        {
            _bus.Subscribe<NavigationItemChanged>(NavigationItemChangedEventHandler);
        }

        private void NavigationItemChangedEventHandler(MessageArgs message)
        {
            var eventItem = message.GetMessage<NavigationItemChanged>();
            if (eventItem == null || string.IsNullOrEmpty(eventItem.ComponentLabels))
            {
                return;
            }

            RenderDynamicComponent(_dynamicComponentFactory.Create(eventItem.ComponentLabels));
        }

        private void RenderDynamicComponent(DynamicComponentData dynamicComponentData)
        {
            if (_selectedComponentType == dynamicComponentData.Type)
            {
                return;
            }

            _centerHeader = dynamicComponentData.Title;
            _selectedComponentType = dynamicComponentData.Type;
            _selectedComponentParameters = dynamicComponentData.Parameters;

            StateHasChanged();
        }

        public void Dispose()
        {
            _bus.UnSubscribe<NavigationItemChanged>(NavigationItemChangedEventHandler);
        }
    }
}
