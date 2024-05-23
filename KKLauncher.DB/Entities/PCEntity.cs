namespace KKLauncher.DB.Entities
{
    public class PCEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LocalIp { get; set; }

        public string Password { get; set; }

        public string SteamPath { get; set; }
    }
}
