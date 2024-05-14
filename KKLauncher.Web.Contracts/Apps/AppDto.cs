namespace KKLauncher.Web.Contracts.Apps
{
    public class AppDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Path { get; set; }

        public string SteamId { get; set; }

        public AppDto()
        {

        }

        public AppDto(Guid id, string name, string path, string? steamId = null, byte[]? image = null)
        {
            Id = id;
            Name = name;
            Path = path;
            SteamId = steamId ?? string.Empty;
            Image = image ?? new byte[] { };
        }
    }
}
