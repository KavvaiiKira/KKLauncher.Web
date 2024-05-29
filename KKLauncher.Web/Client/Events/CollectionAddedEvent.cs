namespace KKLauncher.Web.Client.Events
{
    public class CollectionAddedEvent
    {
        public Guid NewCollectionId { get; set; }

        public CollectionAddedEvent(Guid newCollectionId)
        {
            NewCollectionId = newCollectionId;
        }
    }
}
