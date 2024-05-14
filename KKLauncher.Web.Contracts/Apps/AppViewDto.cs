namespace KKLauncher.Web.Contracts.Apps
{
    public class AppViewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public bool IsSteamStartAvailable { get; set; }

        public AppViewDto(Guid id, string name, bool isSteamStartAvailable = false, byte[]? image = null)
        {
            Id = id;
            Name = name;
            IsSteamStartAvailable = isSteamStartAvailable;
            Image = image ?? new byte[] { };
        }
    }
}
