using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.PCs;
using KKLauncher.Web.Contracts.ResponseDtos;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Services
{
    public class PCService : IPCService
    {
        private readonly IAsyncRepository<PCEntity> _pcRepository;
        private readonly ILogger<PCService> _logger;

        public PCService(
            IAsyncRepository<PCEntity> pcRepository,
            ILogger<PCService> logger)
        {
            _pcRepository = pcRepository;
            _logger = logger;
        }

        public async Task<PCLoginResponseDto> LoginPCAsyc(PCDto pcDto)
        {
            var pcEntity = await _pcRepository.FirstOrDefaultAsync(p => p.LocalIp == pcDto.LocalIp);
            if (pcEntity == null)
            {
                _logger.LogInformation($"Login attempt logged for IP: {pcDto.LocalIp}");
                return new PCLoginResponseDto($"PC with IP: {pcDto.LocalIp} does not exist!");
            }

            if (pcEntity.Password != pcDto.Password)
            {
                _logger.LogInformation($"Login attempt with incorrect password for IP: {pcDto.LocalIp}");
                return new PCLoginResponseDto($"Wrong password!");
            }

            _logger.LogInformation($"Successful login for IP: {pcDto.LocalIp}");

            return new PCLoginResponseDto();
        }
    }
}
