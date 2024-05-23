namespace KKLauncher.DB.Entities
{
    public class CollectionEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid PCId { get; set; }

        public PCEntity PC { get; set; }

        public List<AppEntity> Apps { get; set; } = new List<AppEntity>();
    }
}
