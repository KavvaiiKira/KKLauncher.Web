namespace KKLauncher.Web.Client.Events
{
    public class AppDeletedEvent
    {
        public Guid DeletedAppId { get; set; }

        public AppDeletedEvent(Guid deletedAppId)
        {
            DeletedAppId = deletedAppId;
        }
    }
}
