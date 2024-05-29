namespace KKLauncher.Web.Contracts.ResponseDtos
{
    public class AppStartResultDto
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public AppStartResultDto()
        {
            Success = false;
            Message = string.Empty;
        }

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
