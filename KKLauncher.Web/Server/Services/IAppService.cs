﻿using KKLauncher.Web.Contracts.Apps;

namespace KKLauncher.Web.Server.Services
{
    public interface IAppService
    {
        Task<bool> AddAppAsync(AppDto app);
    }
}
