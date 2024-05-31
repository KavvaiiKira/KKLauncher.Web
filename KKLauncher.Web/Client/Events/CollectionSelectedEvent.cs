namespace KKLauncher.Web.Client.Events
{
    public class CollectionSelectedEvent
    {
        public Guid CollectionId { get; set; }

        public string CollectionName { get; set; }

        public CollectionSelectedEvent(Guid collectionId, string collectionName)
        {
            CollectionId = collectionId;
            CollectionName = collectionName;
        }
    }
}
