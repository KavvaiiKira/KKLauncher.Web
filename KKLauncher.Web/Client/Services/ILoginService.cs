using KKLauncher.Web.Contracts;

namespace KKLauncher.Web.Client.Services
{
    public interface ILoginService
    {
        Task<PCLoginResponseDto> LoginPcAsync(PCDto pcDto);
    }
}
