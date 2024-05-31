using BlazorComponentBus;
using KKLauncher.Web.Client.Constants;
using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Client.Pages.DynamicComponents;

namespace KKLauncher.Web.Client.Pages.Main
{
    public partial class MainComponent
    {
        private string _centerHeader = string.Empty;
        private Type? _selectedComponentType = null;
        private Dictionary<string, object>? _selectedComponentParameters = null;

        protected override void OnParametersSet()
        {
            _bus.Subscribe<ApplicationsMenuItemSelectedEvent>(ApplicationsMenuItemSelectedEventHandler);
            _bus.Subscribe<CollectionSelectedEvent>(CollectionSelectedEventHandler);
        }

        private void ApplicationsMenuItemSelectedEventHandler(MessageArgs message)
        {
            RenderDynamicComponent(
                new DynamicComponentData(
                    typeof(AppsComponent),
                    ComponentConstants.Titles.ApplicationsTitle));
        }

        private void CollectionSelectedEventHandler(MessageArgs message)
        {
            var eventItem = message.GetMessage<CollectionSelectedEvent>();
            if (eventItem == null)
            {
                return;
            }

            RenderDynamicComponent(
                new DynamicComponentData(
                    typeof(CollectionAppsComponent),
                    eventItem.CollectionName,
                    new Dictionary<string, object>()
                    {
                        { nameof(CollectionAppsComponent.CollectionId), eventItem.CollectionId }
                    }));
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
            _bus.UnSubscribe<ApplicationsMenuItemSelectedEvent>(ApplicationsMenuItemSelectedEventHandler);
            _bus.UnSubscribe<CollectionSelectedEvent>(CollectionSelectedEventHandler);
        }
    }
}
