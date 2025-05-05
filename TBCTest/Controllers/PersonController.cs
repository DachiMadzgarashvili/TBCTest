using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TBCTest.Managers;
using TBCTest.Models.DTOs;

namespace TBCTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonManager _manager;
        public PersonController(IPersonManager manager) => _manager = manager;

        [HttpGet]
        [SwaggerOperation(Summary = "List or search persons", Description = "Quick and detailed search with paging. Returns X-Total-Count header.")]
        [SwaggerResponse(200, "Matched persons list", typeof(IEnumerable<PersonDto>))]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll([FromQuery] PersonSearchParams p)
        {
            var (items, total) = await _manager.SearchAsync(p);
            Response.Headers["X-Total-Count"] = total.ToString();
            return Ok(items);
        }

        [HttpGet("{id}")]
        [SwaggerOperation("Get person by ID")]
        [SwaggerResponse(200, "Person found", typeof(PersonDto))]
        [SwaggerResponse(404, "Person not found")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _manager.GetByIdAsync(id);
            return person == null ? NotFound() : Ok(person);
        }

        [HttpPost]
        [SwaggerOperation("Create a new person")]
        [SwaggerResponse(201, "Person created", typeof(PersonDto))]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonDto dto)
        {
            var created = await _manager.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [SwaggerOperation("Update an existing person")]
        [SwaggerResponse(204, "Person updated")]
        [SwaggerResponse(404, "Person not found")]
        [SwaggerResponse(400, "Invalid input data")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePersonDto dto)
        {
            var success = await _manager.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation("Delete a person")]
        [SwaggerResponse(204, "Person deleted")]
        [SwaggerResponse(404, "Person not found")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _manager.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("relation-report")]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any)]
        [SwaggerOperation("Get related-person report", "Counts of related people by type for each person.")]
        [SwaggerResponse(200, "Report data", typeof(IEnumerable<PersonRelationReportDto>))]
        public async Task<ActionResult<List<PersonRelationReportDto>>> GetRelationReport()
        {
            var report = await _manager.GetRelationReportAsync();
            return Ok(report);
        }

        [HttpPost("relation")]
        [SwaggerOperation("Add a person relation")]
        [SwaggerResponse(200, "Relation added")]
        [SwaggerResponse(400, "Invalid request or not found")]
        public async Task<IActionResult> AddRelation([FromBody] CreateRelationDto dto)
        {
            var (ok, msg) = await _manager.AddRelationAsync(dto);
            return ok ? Ok(msg) : BadRequest(msg);
        }

        [HttpDelete("relation")]
        [SwaggerOperation("Remove a person relation")]
        [SwaggerResponse(200, "Relation removed")]
        [SwaggerResponse(404, "Relation not found")]
        public async Task<IActionResult> RemoveRelation([FromQuery] int personId, [FromQuery] int relatedPersonId)
        {
            var (ok, msg) = await _manager.RemoveRelationAsync(personId, relatedPersonId);
            return ok ? Ok(msg) : NotFound(msg);
        }

        [HttpPost("{id}/upload-image")]
        [SwaggerOperation("Upload or update a person image")]
        [SwaggerResponse(200, "Image uploaded successfully")]
        [SwaggerResponse(400, "Invalid file or person not found")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var (ok, msg, path) = await _manager.UploadImageAsync(id, file);
            return ok ? Ok(new { imageUrl = path }) : BadRequest(msg);
        }

        [HttpDelete("{id}/remove-image")]
        [SwaggerOperation("Remove a person image")]
        [SwaggerResponse(200, "Image removed successfully")]
        [SwaggerResponse(404, "Person or image not found")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            var (ok, msg) = await _manager.RemoveImageAsync(id);
            return ok ? Ok(msg) : NotFound(msg);
        }
    }
}
