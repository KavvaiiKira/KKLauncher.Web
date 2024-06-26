﻿using KKLauncher.Web.Contracts.Apps;

namespace KKLauncher.Web.Server.Services
{
    public interface IAppService
    {
        Task<bool> AddAppAsync(AppDto app);

        Task<IEnumerable<AppViewDto>> GetAppsByPCLocalIpAsync(string pcLocalIp);

        Task<AppViewDto?> GetAppViewByIdAsync(Guid appId);

        Task<IEnumerable<AppViewDto>> SearchAppsAsync(string localIp, string appNameContainsKey);

        Task<bool> RemoveAppAsync(Guid appId);
    }
}
