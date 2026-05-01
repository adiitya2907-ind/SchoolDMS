using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Models.DTOs.Documents;
using SchoolDMS.API.Services.Interfaces;
using System.Security.Claims;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }

        [HttpPost("upload")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> UploadDocument([FromForm] DocumentUploadDTO request)
        {
            var userId = GetCurrentUserId();
            var result = await _documentService.UploadDocumentAsync(request, userId);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetDocumentById), new { id = result.Data?.DocumentId }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var result = await _documentService.GetDocumentByIdAsync(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpGet("by-visit/{visitId}")]
        public async Task<IActionResult> GetDocumentsByVisitId(int visitId)
        {
            var result = await _documentService.GetDocumentsByVisitIdAsync(visitId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Engineer")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var userId = GetCurrentUserId();
            var result = await _documentService.DeleteDocumentAsync(id, userId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("{id}/verify")]
        [Authorize(Roles = "OpsVerifier,Admin")]
        public async Task<IActionResult> VerifyDocument(int id, [FromQuery] bool isVerified)
        {
            var verifierId = GetCurrentUserId();
            var result = await _documentService.VerifyDocumentAsync(id, isVerified, verifierId);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        // Full-text search placeholder
        [HttpGet("search")]
        public IActionResult SearchDocuments([FromQuery] string query)
        {
            return Ok("Search functionality placeholder");
        }
    }
}
