namespace KKLauncher.Web.Contracts.Collections
{
    public class CollectionViewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public CollectionViewDto(Guid id, string name, byte[]? image = null)
        {
            Id = id;
            Name = name;
            Image = image ?? new byte[] { };
        }
    }
}
