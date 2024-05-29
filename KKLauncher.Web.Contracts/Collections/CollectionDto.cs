namespace KKLauncher.Web.Contracts.Collections
{
    public class CollectionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string PCLocalIp { get; set; }

        public List<Guid> AppIds { get; set; }
    }
}
