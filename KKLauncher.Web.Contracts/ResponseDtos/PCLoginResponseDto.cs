namespace KKLauncher.Web.Contracts.ResponseDtos
{
    public class PCLoginResponseDto
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public PCLoginResponseDto()
        {
            Success = true;
            Message = string.Empty;
        }

        public PCLoginResponseDto(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
