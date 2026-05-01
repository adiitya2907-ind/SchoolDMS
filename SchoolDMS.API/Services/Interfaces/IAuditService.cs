using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IAuditService
    {
        Task LogActionAsync(int? userId, string action, string tableName, int? recordId, string? oldValues, string? newValues);
        Task<PaginatedResponse<AuditLog>> GetAuditLogsAsync(int pageNumber = 1, int pageSize = 20);
        Task<ApiResponse<IEnumerable<AuditLog>>> GetLogsByUserAsync(int userId);
        Task<ApiResponse<IEnumerable<AuditLog>>> GetLogsByRecordAsync(string tableName, int recordId);
    }
}
