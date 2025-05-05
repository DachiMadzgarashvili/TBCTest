using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Managers;
using TBCTest.Models.DTOs;

namespace TBCTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityManager _manager;

        public CityController(ICityManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get all cities
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAll()
        {
            return Ok(await _manager.GetAllAsync());
        }

        /// <summary>
        /// Get a city by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var city = await _manager.GetByIdAsync(id);
            return city == null ? NotFound() : Ok(city);
        }

        /// <summary>
        /// Create a new city
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CityDto>> Create([FromBody] CreateCityDto dto)
        {
            var created = await _manager.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// Update an existing city
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCityDto dto)
        {
            var result = await _manager.UpdateAsync(id, dto);
            return result.Success ? NoContent() : NotFound(result.Message);
        }

        /// <summary>
        /// Delete a city by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manager.DeleteAsync(id);
            return result.Success ? NoContent() : NotFound(result.Message);
        }
    }
}
