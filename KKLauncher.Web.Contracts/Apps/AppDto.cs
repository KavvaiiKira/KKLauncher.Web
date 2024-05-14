namespace KKLauncher.Web.Contracts.Apps
{
    public class AppDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Path { get; set; }

        public string SteamId { get; set; }

        public string PCLocalIp { get; set; }
    }
}
