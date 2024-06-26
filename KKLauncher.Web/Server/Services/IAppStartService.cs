﻿using KKLauncher.Web.Contracts.ResponseDtos;

namespace KKLauncher.Web.Server.Services
{
    public interface IAppStartService
    {
        Task<AppStartResultDto> StartAppAsync(Guid appId);
    }
}
