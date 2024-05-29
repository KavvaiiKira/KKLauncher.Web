using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKLauncher.Web.Server.Controllers
{
    [Route("api/kk/v1/app")]
    public class AppController : ControllerBase
    {
        private readonly IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAppAsync([FromBody] AppDto appDto)
        {
            if (appDto == null)
            {
                return BadRequest("Requst BODY must not be NULL!");
            }

            var res = await _appService.AddAppAsync(appDto);

            return Ok(res);
        }

        [HttpGet("pcapps/{localIp}")]
        [ProducesResponseType(typeof(IEnumerable<AppViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicationsByPCLocalIpAsync(string localIp)
        {
            if (string.IsNullOrEmpty(localIp))
            {
                return BadRequest("Requst BODY must not be NULL!");
            }

            var res = await _appService.GetAppsByPCLocalIpAsync(localIp);

            return Ok(res);
        }

        [HttpGet("appsearch/{ip}/{appNameKey}")]
        [ProducesResponseType(typeof(IEnumerable<AppViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchApplicationsAsync(string ip, string appNameKey)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return BadRequest("Local ip for app search must not be NULL or EMPTY!");
            }

            if (string.IsNullOrEmpty(appNameKey))
            {
                return BadRequest("App name search pattern must not be NULL or EMPTY!");
            }

            var res = await _appService.SearchAppsAsync(ip, appNameKey);

            return Ok(res);
        }

        [HttpGet("appview/{id}")]
        [ProducesResponseType(typeof(AppViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAppViewByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var appId))
            {
                return BadRequest("Application ID must be GUID!");
            }

            var res = await _appService.GetAppViewByIdAsync(appId);

            return Ok(res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveAppAsync(string id)
        {
            if (!Guid.TryParse(id, out var appId))
            {
                return BadRequest("Application ID must be GUID!");
            }

            var res = await _appService.RemoveAppAsync(appId);

            return Ok(res);
        }
    }
}
