using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditService _auditService;

        public AuditLogsController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuditLogs([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _auditService.GetAuditLogsAsync(pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetAuditLogsByUser(int userId)
        {
            var result = await _auditService.GetLogsByUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("by-record")]
        public async Task<IActionResult> GetAuditLogsByRecord([FromQuery] string tableName, [FromQuery] int recordId)
        {
            var result = await _auditService.GetLogsByRecordAsync(tableName, recordId);
            return Ok(result);
        }
    }
}
