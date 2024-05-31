using KKLauncher.Web.Client.Events;

namespace KKLauncher.Web.Client.Pages.NavigationComponents
{
    public partial class NavigationMenu
    {
        private bool _addCollectionShown = false;

        private async Task ApplicationsSelectionChanged(EventArgs args)
        {
            await _bus.Publish(new ApplicationsMenuItemSelectedEvent());
        }
    }
}
