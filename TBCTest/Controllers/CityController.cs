using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TBCTest.Managers;
using TBCTest.Models.DTOs;

namespace TBCTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityManager _manager;
        public CityController(ICityManager manager) => _manager = manager;

        [HttpGet]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
        [SwaggerOperation(Summary = "List all cities")]
        [SwaggerResponse(200, "List of cities", typeof(IEnumerable<CityDto>))]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAll()
            => Ok(await _manager.GetAllAsync());

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get city by id")]
        [SwaggerResponse(200, "City found", typeof(CityDto))]
        [SwaggerResponse(404, "City not found")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var city = await _manager.GetByIdAsync(id);
            return city == null ? NotFound() : Ok(city);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new city")]
        [SwaggerResponse(201, "City created", typeof(CityDto))]
        public async Task<ActionResult<CityDto>> Create([FromBody] CreateCityDto dto)
        {
            var created = await _manager.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update city")]
        [SwaggerResponse(204, "City updated")]
        [SwaggerResponse(404, "City not found")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCityDto dto)
        {
            var (ok, msg) = await _manager.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound(msg);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete city")]
        [SwaggerResponse(204, "City deleted")]
        [SwaggerResponse(404, "City not found")]
        public async Task<IActionResult> Delete(int id)
        {
            var (ok, msg) = await _manager.DeleteAsync(id);
            return ok ? NoContent() : NotFound(msg);
        }
    }
}
