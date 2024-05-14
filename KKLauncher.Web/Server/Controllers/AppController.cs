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
    }
}
