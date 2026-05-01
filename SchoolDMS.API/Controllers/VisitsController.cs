using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Services.Interfaces;
using System.Security.Claims;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitService _visitService;

        public VisitsController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }

        [HttpGet]
        public async Task<IActionResult> GetVisits([FromQuery] int? engineerId, [FromQuery] int? schoolId, [FromQuery] int? statusId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            // If User is Engineer, force filter to their own visits
            if (User.IsInRole("Engineer"))
            {
                engineerId = GetCurrentUserId();
            }

            return Ok(await _visitService.GetVisitsAsync(engineerId, schoolId, statusId, pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVisitById(int id)
        {
            var result = await _visitService.GetVisitByIdAsync(id);
            if (!result.Success) return NotFound(result);

            // Access check
            if (User.IsInRole("Engineer") && result.Data?.EngineerId != GetCurrentUserId())
            {
                return Forbid();
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> CreateVisit([FromBody] CreateVisitDTO request)
        {
            var engineerId = GetCurrentUserId();
            var result = await _visitService.CreateVisitAsync(request, engineerId);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetVisitById), new { id = result.Data }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> UpdateVisit(int id, [FromBody] CreateVisitDTO request)
        {
            var engineerId = GetCurrentUserId();
            var result = await _visitService.UpdateVisitAsync(id, request, engineerId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var engineerId = GetCurrentUserId();
            var result = await _visitService.DeleteVisitAsync(id, engineerId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("by-engineer/{engineerId}")]
        public async Task<IActionResult> GetVisitsByEngineer(int engineerId)
        {
            if (User.IsInRole("Engineer") && engineerId != GetCurrentUserId()) return Forbid();
            
            return Ok(await _visitService.GetVisitsAsync(engineerId: engineerId));
        }

        [HttpGet("pending-verification")]
        [Authorize(Roles = "OpsVerifier,Admin")]
        public async Task<IActionResult> GetPendingVerifications()
        {
            return Ok(await _visitService.GetPendingVerificationsAsync());
        }

        // Actions

        [HttpPatch("{id}/check-in")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> CheckIn(int id, [FromQuery] decimal latitude, [FromQuery] decimal longitude)
        {
            var engineerId = GetCurrentUserId();
            var result = await _visitService.CheckInAsync(id, engineerId, latitude, longitude);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("{id}/check-out")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> CheckOut(int id)
        {
            var engineerId = GetCurrentUserId();
            var result = await _visitService.CheckOutAsync(id, engineerId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("{id}/submit")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> SubmitVisit(int id)
        {
            var engineerId = GetCurrentUserId();
            var result = await _visitService.SubmitVisitAsync(id, engineerId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}
