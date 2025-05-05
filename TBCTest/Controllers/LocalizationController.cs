using Microsoft.AspNetCore.Mvc;
using TBCTest.Managers;
using TBCTest.Models;

namespace TBCTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationController : ControllerBase
    {
        private readonly ILocalizationManager _manager;

        public LocalizationController(ILocalizationManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get all localization entries
        /// </summary>
        [HttpGet]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<IEnumerable<Localization>>> GetAll()
        {
            return Ok(await _manager.GetAllAsync());
        }

        /// <summary>
        /// Get all translations by key
        /// </summary>
        [HttpGet("by-key")]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
        public async Task<ActionResult<IEnumerable<Localization>>> GetByKey([FromQuery] string key)
        {
            var results = await _manager.GetByKeyAsync(key);
            return results.Count == 0 ? NotFound() : Ok(results);
        }

        /// <summary>
        /// Update a translation entry
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Localization updated)
        {
            var result = await _manager.UpdateAsync(id, updated);
            return result.Success ? NoContent() : NotFound(result.Message);
        }
    }
}
