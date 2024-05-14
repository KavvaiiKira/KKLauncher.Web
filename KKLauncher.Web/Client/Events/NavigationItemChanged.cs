namespace KKLauncher.Web.Client.Events
{
    public class NavigationItemChanged
    {
        public string ComponentLabels { get; protected set; }

        public NavigationItemChanged(string componentLabels)
        {
            ComponentLabels = componentLabels;
        }
    }
}
