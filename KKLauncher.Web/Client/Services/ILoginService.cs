using KKLauncher.Web.Contracts.PCs;
using KKLauncher.Web.Contracts.ResponseDtos;

namespace KKLauncher.Web.Client.Services
{
    public interface ILoginService
    {
        Task<PCLoginResponseDto> LoginPcAsync(PCDto pcDto);
    }
}
