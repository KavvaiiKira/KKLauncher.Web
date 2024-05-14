namespace KKLauncher.Web.Contracts
{
    public class AppStartResultDto
    {
        public bool Success { get; private set; }

        public string Message { get; private set; }

        public AppStartResultDto(bool success, string? message = null)
        {
            Success = success;
            Message = message ?? string.Empty;
        }

        public AppStartResultDto(string message)
        {
            Message = message;
            Success = false;
        }
    }
}
