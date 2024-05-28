using KKLauncher.Web.Contracts.PCs;
using KKLauncher.Web.Contracts.ResponseDtos;
using KKLauncher.Web.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKLauncher.Web.Server.Controllers
{
    [Route("api/kk/v1/pc")]
    public class PCController : ControllerBase
    {
        private readonly IPCService _pcService;

        public PCController(IPCService pcService)
        {
            _pcService = pcService;
        }

        [HttpPut("login")]
        [ProducesResponseType(typeof(PCLoginResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginPCAsyc([FromBody] PCDto pcDto)
        {
            if (pcDto == null)
            {
                return BadRequest("Requst BODY must not be NULL!");
            }

            var res = await _pcService.LoginPCAsyc(pcDto);

            return Ok(res);
        }
    }
}
