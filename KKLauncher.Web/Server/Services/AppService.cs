using AutoMapper;
using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Server.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.Services
{
    public class AppService : IAppService
    {
        private readonly IAsyncRepository<AppEntity> _appRepository;
        private readonly IAsyncRepository<PCEntity> _pcRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppService> _logger;

        public AppService(
            IAsyncRepository<AppEntity> appRepository,
            IAsyncRepository<PCEntity> pcRepository,
            IMapper mapper,
            ILogger<AppService> logger
            )
        {
            _appRepository = appRepository;
            _pcRepository = pcRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddAppAsync(AppDto app)
        {
            try
            {
                var pcEntity = await _pcRepository.FirstOrDefaultAsync(pc => pc.LocalIp == app.PCLocalIp);
                if (pcEntity == null)
                {
                    throw new ArgumentNullException($"PC with local IP: {app.PCLocalIp} not found!");
                }

                var appEntity = _mapper.Map<AppEntity>(app);
                appEntity.PCId = pcEntity.Id;

                await _appRepository.AddAsync(appEntity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding new Applicaton. Error message: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<AppViewDto>> GetApplicationsByPCLocalIpAsync(string pcLocalIp)
        {
            try
            {
                var pcEntity = await _pcRepository.FirstOrDefaultAsync(pc => pc.LocalIp == pcLocalIp);
                if (pcEntity == null)
                {
                    throw new ArgumentNullException($"PC with local IP: {pcLocalIp} not found!");
                }

                var appEntities = await _appRepository.GetAll().Where(a => a.PCId == pcEntity.Id).ToListAsync();

                return appEntities.Select(_mapper.Map<AppViewDto>);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting Applicatons of PC with local IP: {pcLocalIp}. Error message: {ex.Message}");
                return Enumerable.Empty<AppViewDto>();
            }
        }

        public async Task<AppViewDto?> GetAppViewByIdAsync(Guid appId)
        {
            try
            {
                var appEntity = await _appRepository.FirstOrDefaultAsync(a => a.Id == appId);

                return appEntity != null ?
                    _mapper.Map<AppViewDto>(appEntity) :
                    throw new ArgumentNullException($"Application with ID: {appId} not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting Applicaton with ID: {appId}. Error message: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> RemoveAppAsync(Guid appId)
        {
            try
            {
                var existApp = await _appRepository.FirstOrDefaultAsync(a => a.Id == appId);
                if (existApp == null)
                {
                    throw new ArgumentNullException($"Application with ID: {appId} not found!");
                }

                await _appRepository.RemoveAsync(existApp);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while removing Applicaton. Error message: {ex.Message}");
                return false;
            }
        }
    }
}
