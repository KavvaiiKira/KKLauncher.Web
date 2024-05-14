using AutoMapper;
using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Server.Repositories.Base;

namespace KKLauncher.Web.Server.Services
{
    public class AppService : IAppService
    {
        private readonly IAsyncRepository<AppEntity> _appRepository;
        private readonly IAsyncRepository<PCEntity> _pcRepository;
        private readonly IMapper _mapper;

        public AppService(
            IAsyncRepository<AppEntity> appRepository,
            IAsyncRepository<PCEntity> pcRepository,
            IMapper mapper
            )
        {
            _appRepository = appRepository;
            _pcRepository = pcRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddAppAsync(AppDto app)
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
    }
}
