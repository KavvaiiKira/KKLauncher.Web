using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Contracts.Collections;

namespace KKLauncher.Web.Client.Services
{
    public interface ICollectionService
    {
        Task<bool> AddCollectionAsync(CollectionDto collectionDto);

        Task<IEnumerable<CollectionViewDto>> GetCollectionsByPCLocalIpAsync(string pcLocalIp);

        Task<CollectionViewDto?> GetCollectionViewByIdAsync(Guid collectionId);

        Task<IEnumerable<AppViewDto>> GetCollectionAppsAsync(Guid collectionId);
    }
}
