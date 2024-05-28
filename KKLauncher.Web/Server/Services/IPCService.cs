using KKLauncher.Web.Contracts.PCs;
using KKLauncher.Web.Contracts.ResponseDtos;

namespace KKLauncher.Web.Server.Services
{
    public interface IPCService
    {
        Task<PCLoginResponseDto> LoginPCAsyc(PCDto pcDto);
    }
}
