namespace KKLauncher.DB.Entities
{
    public class AppEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Path { get; set; }

        public string SteamId { get; set; }

        public Guid PCId { get; set; }

        public PCEntity PC { get; set; }

        public List<CollectionEntity> Collections { get; set; } = new List<CollectionEntity>();
    }
}
