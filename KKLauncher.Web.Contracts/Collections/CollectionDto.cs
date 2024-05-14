namespace KKLauncher.Web.Contracts.Collections
{
    public class CollectionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public List<Guid> Apps { get; set; }

        public CollectionDto()
        {
            
        }

        public CollectionDto(Guid id, string name, List<Guid> apps, byte[]? image = null)
        {
            Id = id;
            Name = name;
            Apps = apps;
            Image = image ?? new byte[] { };
        }
    }
}
