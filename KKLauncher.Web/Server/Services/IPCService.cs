using KKLauncher.Web.Contracts;

namespace KKLauncher.Web.Server.Services
{
    public interface IPCService
    {
        Task<PCLoginResponseDto> LoginPCAsyc(PCDto pcDto);
    }
}
