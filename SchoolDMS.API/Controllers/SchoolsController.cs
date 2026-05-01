using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Models.DTOs.Schools;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchools([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            return Ok(await _schoolService.GetAllSchoolsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolById(int id)
        {
            var result = await _schoolService.GetSchoolByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchSchools([FromQuery] string searchTerm)
        {
            return Ok(await _schoolService.SearchSchoolsAsync(searchTerm));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSchool([FromBody] CreateSchoolDTO request)
        {
            var result = await _schoolService.CreateSchoolAsync(request);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetSchoolById), new { id = result.Data }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSchool(int id, [FromBody] UpdateSchoolDTO request)
        {
            var result = await _schoolService.UpdateSchoolAsync(id, request);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            var result = await _schoolService.DeleteSchoolAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}
