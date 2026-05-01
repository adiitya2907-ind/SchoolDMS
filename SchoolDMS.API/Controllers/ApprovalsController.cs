using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Services.Interfaces;
using System.Security.Claims;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "OpsVerifier,Admin")]
    public class ApprovalsController : ControllerBase
    {
        private readonly IApprovalService _approvalService;
        private readonly IVisitService _visitService;

        public ApprovalsController(IApprovalService approvalService, IVisitService visitService)
        {
            _approvalService = approvalService;
            _visitService = visitService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingApprovals()
        {
            var result = await _visitService.GetPendingVerificationsAsync();
            return Ok(result);
        }

        [HttpPost("{visitId}/approve")]
        public async Task<IActionResult> ApproveVisit(int visitId, [FromBody] VisitApprovalDTO request)
        {
            request.IsApproved = true;
            var verifierId = GetCurrentUserId();
            var result = await _approvalService.ProcessApprovalAsync(visitId, verifierId, request);
            
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("{visitId}/reject")]
        public async Task<IActionResult> RejectVisit(int visitId, [FromBody] VisitApprovalDTO request)
        {
            request.IsApproved = false;
            var verifierId = GetCurrentUserId();
            var result = await _approvalService.ProcessApprovalAsync(visitId, verifierId, request);

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("history/{visitId}")]
        public async Task<IActionResult> GetApprovalHistory(int visitId)
        {
            var result = await _approvalService.GetApprovalHistoryAsync(visitId);
            return Ok(result);
        }
    }
}
