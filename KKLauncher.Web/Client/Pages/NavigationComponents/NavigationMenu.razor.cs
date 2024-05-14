using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Constants;

namespace KKLauncher.Web.Client.Pages.NavigationComponents
{
    public partial class NavigationMenu
    {
        private async Task ApplicationsSelectionChanged(EventArgs args)
        {
            await _bus.Publish(new NavigationItemChanged(ComponentConstants.Keys.ApplicationsComponent));
        }

        private async Task CollectionsSelectionChanged(EventArgs args)
        {
            await _bus.Publish(new NavigationItemChanged(ComponentConstants.Keys.CollectionsComponent));
        }
    }
}
