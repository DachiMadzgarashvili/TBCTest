using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBCTest.Models.DTOs;
using TBCTest.Managers;
using TBCTest.Models;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            var people = await _manager.GetAllAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await _manager.GetByIdAsync(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonDto dto)
        {
            var created = await _manager.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreatePersonDto dto)
        {
            var success = await _manager.UpdateAsync(id, dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _manager.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet("relation-report")]
        public async Task<ActionResult<List<PersonRelationReportDto>>> GetRelationReport()
        {
            var report = await _manager.GetRelationReportAsync();
            return Ok(report);
        }

        [HttpPost("relation")]
        public async Task<IActionResult> AddRelation([FromBody] CreateRelationDto dto)
        {
            var success = await _manager.AddRelationAsync(dto);
            return success ? Ok() : BadRequest();
        }

        [HttpDelete("relation")]
        public async Task<IActionResult> RemoveRelation([FromQuery] int personId, [FromQuery] int relatedPersonId)
        {
            var success = await _manager.RemoveRelationAsync(personId, relatedPersonId);
            return success ? Ok() : NotFound();
        }


        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            var person = await _manager.GetEntityAsync(id);
            if (person == null)
                return NotFound("Person not found.");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            Directory.CreateDirectory(uploadsFolder);

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            try
            {
                // Save new image
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                // Delete old image (if exists)
                if (!string.IsNullOrEmpty(person.ImagePath))
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", person.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                // Save new image path
                person.ImagePath = $"/images/{fileName}";
                await _manager.UpdateImagePathAsync(person);

                return Ok(new { imageUrl = person.ImagePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Image upload failed: {ex.Message}");
            }
        }
        [HttpDelete("{id}/remove-image")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            var person = await _manager.GetEntityAsync(id);
            if (person == null || string.IsNullOrEmpty(person.ImagePath))
                return NotFound("No image to delete.");

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", person.ImagePath.TrimStart('/'));

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            person.ImagePath = null;
            await _manager.UpdateImagePathAsync(person);

            return Ok("Image deleted.");
        }

    }
}
