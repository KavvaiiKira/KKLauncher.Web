using KKLauncher.Web.Contracts.ResponseDtos;
using KKLauncher.Web.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKLauncher.Web.Server.Controllers
{
    [Route("api/kk/v1/appstarter")]
    public class AppStartController : ControllerBase
    {
        private readonly IAppStartService _appStartService;

        public AppStartController(IAppStartService appStartService)
        {
            _appStartService = appStartService;
        }

        [HttpPut]
        [ProducesResponseType(typeof(AppStartResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> StartAppAsync([FromBody] Guid? appId)
        {
            if (appId == null)
            {
                return BadRequest("Application ID must be GUID!");
            }

            var res = await _appStartService.StartAppAsync(appId.Value);

            return Ok(res);
        }
    }
}
