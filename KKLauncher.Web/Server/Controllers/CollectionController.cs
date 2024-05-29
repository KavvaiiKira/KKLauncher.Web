using KKLauncher.Web.Contracts.Collections;
using KKLauncher.Web.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKLauncher.Web.Server.Controllers
{
    [Route("api/kk/v1/collection")]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPut]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCollectionAsync([FromBody] CollectionDto collectionDto)
        {
            if (collectionDto == null)
            {
                return BadRequest("Requst BODY must not be NULL!");
            }

            var res = await _collectionService.AddCollectionAsync(collectionDto);

            return Ok(res);
        }

        [HttpGet("pccollections/{localIp}")]
        [ProducesResponseType(typeof(IEnumerable<CollectionViewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCollectionsByPCLocalIpAsync(string localIp)
        {
            if (string.IsNullOrEmpty(localIp))
            {
                return BadRequest("Requst BODY must not be NULL!");
            }

            var res = await _collectionService.GetCollectionsByPCLocalIpAsync(localIp);

            return Ok(res);
        }

        [HttpGet("collectionview/{id}")]
        [ProducesResponseType(typeof(CollectionViewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCollectionViewByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var appId))
            {
                return BadRequest("Collection ID must be GUID!");
            }

            var res = await _collectionService.GetCollectionViewByIdAsync(appId);

            return Ok(res);
        }
    }
}
