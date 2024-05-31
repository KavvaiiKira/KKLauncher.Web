using AutoMapper;
using KKLauncher.DB.Entities;
using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Contracts.Collections;
using KKLauncher.Web.Server.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly IAsyncRepository<CollectionEntity> _collectionRepository;
        private readonly IAsyncRepository<AppCollectionEntity> _appCollectionRepository;
        private readonly IAsyncRepository<AppEntity> _appRepository;
        private readonly IAsyncRepository<PCEntity> _pcRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppService> _logger;

        public CollectionService(
            IAsyncRepository<CollectionEntity> collectionRepository,
            IAsyncRepository<AppCollectionEntity> appCollectionRepository,
            IAsyncRepository<AppEntity> appRepository,
            IAsyncRepository<PCEntity> pcRepository,
            IMapper mapper,
            ILogger<AppService> logger
            )
        {
            _collectionRepository = collectionRepository;
            _appCollectionRepository = appCollectionRepository;
            _appRepository = appRepository;
            _pcRepository = pcRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddCollectionAsync(CollectionDto collectionDto)
        {
            using var transaction = await _collectionRepository.BeginTransactionAsync();
            try
            {
                var pcEntity = await _pcRepository.FirstOrDefaultAsync(pc => pc.LocalIp == collectionDto.PCLocalIp);
                if (pcEntity == null)
                {
                    throw new ArgumentNullException($"PC with local IP: {collectionDto.PCLocalIp} not found!");
                }

                var collectionEntity = _mapper.Map<CollectionEntity>(collectionDto);
                collectionEntity.PCId = pcEntity.Id;

                await _collectionRepository.AddAsync(collectionEntity);

                var appCollectionEntities = collectionDto.AppIds
                    .Select(ai =>
                        new AppCollectionEntity()
                        {
                            AppId = ai,
                            CollectionId = collectionEntity.Id
                        });

                await _appCollectionRepository.AddRangeAsync(appCollectionEntities);

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding new Collection. Error message: {ex.Message}");

                await transaction.RollbackAsync();

                return false;
            }
        }

        public async Task<IEnumerable<CollectionViewDto>> GetCollectionsByPCLocalIpAsync(string pcLocalIp)
        {
            try
            {
                var pcEntity = await _pcRepository.FirstOrDefaultAsync(pc => pc.LocalIp == pcLocalIp);
                if (pcEntity == null)
                {
                    throw new ArgumentNullException($"PC with local IP: {pcLocalIp} not found!");
                }

                var collectionEntities = await _collectionRepository.GetAll().Where(c => c.PCId == pcEntity.Id).ToListAsync();

                return collectionEntities.Select(_mapper.Map<CollectionViewDto>);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting Collections of PC with local IP: {pcLocalIp}. Error message: {ex.Message}");
                return Enumerable.Empty<CollectionViewDto>();
            }
        }

        public async Task<CollectionViewDto?> GetCollectionViewByIdAsync(Guid collectionId)
        {
            try
            {
                var collectionEntity = await _collectionRepository.FirstOrDefaultAsync(c => c.Id == collectionId);

                return collectionEntity != null ?
                    _mapper.Map<CollectionViewDto>(collectionEntity) :
                    throw new ArgumentNullException($"Collection with ID: {collectionId} not found!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting Collection with ID: {collectionId}. Error message: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<AppViewDto>> GetCollectionAppsAsync(Guid collectionId)
        {
            var collectionEntity = await _collectionRepository.FirstOrDefaultAsync(c => c.Id == collectionId);
            if (collectionEntity == null)
            {
                throw new ArgumentNullException($"Collection with ID: {collectionId} not found!");
            }

            var appCollectionentities = await _appCollectionRepository
                .GetAll()
                .Where(ac => ac.CollectionId == collectionId)
                .ToListAsync();

            if (!appCollectionentities.Any())
            {
                return Enumerable.Empty<AppViewDto>();
            }

            var collectionAppIds = appCollectionentities.Select(ac => ac.AppId);

            var collectionApps = await _appRepository
                .GetAll()
                .Where(a => collectionAppIds.Contains(a.Id))
                .ToListAsync();

            return collectionApps.Select(_mapper.Map<AppViewDto>);
        }
    }
}
