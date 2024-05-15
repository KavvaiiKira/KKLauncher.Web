namespace KKLauncher.Web.Client.Events
{
    public class AppAddedEvent
    {
        public Guid NewAppId { get; set; }

        public AppAddedEvent(Guid newAppId)
        {
            NewAppId = newAppId;
        }
    }
}
