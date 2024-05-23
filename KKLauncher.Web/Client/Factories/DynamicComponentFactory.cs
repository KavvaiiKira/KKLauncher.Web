using KKLauncher.Web.Client.Constants;
using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Client.Pages.DynamicComponents;

namespace KKLauncher.Web.Client.Factories
{
    public class DynamicComponentFactory : IDynamicComponentFactory
    {
        private Dictionary<string, DynamicComponentData> _dynamicComponentsData;

        public DynamicComponentFactory()
        {
            _dynamicComponentsData = new Dictionary<string, DynamicComponentData>()
            {
                {
                    ComponentConstants.Keys.ApplicationsComponent,
                    new DynamicComponentData(
                        typeof(AppsComponent),
                        ComponentConstants.Titles.ApplicationsTitle)
                },
                {
                    ComponentConstants.Keys.CollectionComponent,
                    new DynamicComponentData(
                        typeof(CollectionsComponent),
                        ComponentConstants.Titles.CollectionsTitle)
                }
            };
        }

        public DynamicComponentData Create(string componentKey)
        {
            return _dynamicComponentsData.ContainsKey(componentKey) ?
                _dynamicComponentsData[componentKey] :
                throw new NotImplementedException($"Usupported key of dynamic component! Given key: \"{componentKey}\"");
        }
    }
}
