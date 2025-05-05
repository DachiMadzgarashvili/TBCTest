using Microsoft.AspNetCore.Mvc;
using TBCTest.Managers;
using TBCTest.Models.DTOs;

namespace TBCTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonManager _manager;

        public PersonController(IPersonManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get persons (supports quick/detailed search and paging).
        /// Returns X-Total-Count header.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] PersonSearchParams p)
        {
            var (items, total) = await _manager.SearchAsync(p);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        /// <summary>
        /// Get a person by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _manager.GetByIdAsync(id);
            return person == null ? NotFound() : Ok(person);
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonDto dto)
        {
            var created = await _manager.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// Update an existing person
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePersonDto dto)
        {
            var success = await _manager.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Delete a person by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _manager.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Get related person count per type (report)
        /// </summary>
        [HttpGet("relation-report")]
        public async Task<ActionResult<List<PersonRelationReportDto>>> GetRelationReport()
        {
            var report = await _manager.GetRelationReportAsync();
            return Ok(report);
        }

        /// <summary>
        /// Add a related person
        /// </summary>
        [HttpPost("relation")]
        public async Task<IActionResult> AddRelation([FromBody] CreateRelationDto dto)
        {
            var result = await _manager.AddRelationAsync(dto);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        /// <summary>
        /// Remove a related person
        /// </summary>
        [HttpDelete("relation")]
        public async Task<IActionResult> RemoveRelation([FromQuery] int personId, [FromQuery] int relatedPersonId)
        {
            var result = await _manager.RemoveRelationAsync(personId, relatedPersonId);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        /// <summary>
        /// Upload an image for a person
        /// </summary>
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var result = await _manager.UploadImageAsync(id, file);
            return result.Success
                ? Ok(new { imageUrl = result.NewImagePath })
                : StatusCode(500, result.Message);
        }

        /// <summary>
        /// Remove a person's image
        /// </summary>
        [HttpDelete("{id}/remove-image")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            var result = await _manager.RemoveImageAsync(id);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }
    }
}
