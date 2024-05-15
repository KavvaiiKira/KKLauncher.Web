namespace KKLauncher.Web.Contracts.Apps
{
    public class AppViewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public bool IsSteamStartAvailable { get; set; }
    }
}
