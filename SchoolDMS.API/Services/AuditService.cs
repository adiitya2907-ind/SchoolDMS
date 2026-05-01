using SchoolDMS.API.Data;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SchoolDMS.API.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;

        public AuditService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(int? userId, string action, string tableName, int? recordId, string? oldValues, string? newValues)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                TableName = tableName,
                RecordId = recordId,
                OldValues = oldValues,
                NewValues = newValues,
                CreatedAt = DateTime.UtcNow
            };

            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<PaginatedResponse<AuditLog>> GetAuditLogsAsync(int pageNumber = 1, int pageSize = 20)
        {
            var query = _context.AuditLogs.Include(a => a.User).OrderByDescending(a => a.CreatedAt);
            var totalRecords = await query.CountAsync();
            var logs = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return PaginatedResponse<AuditLog>.SuccessResponse(logs, pageNumber, pageSize, totalRecords);
        }

        public async Task<ApiResponse<IEnumerable<AuditLog>>> GetLogsByUserAsync(int userId)
        {
            var logs = await _context.AuditLogs.Where(a => a.UserId == userId).OrderByDescending(a => a.CreatedAt).ToListAsync();
            return ApiResponse<IEnumerable<AuditLog>>.SuccessResponse(logs);
        }

        public async Task<ApiResponse<IEnumerable<AuditLog>>> GetLogsByRecordAsync(string tableName, int recordId)
        {
            var logs = await _context.AuditLogs
                .Where(a => a.TableName == tableName && a.RecordId == recordId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
            return ApiResponse<IEnumerable<AuditLog>>.SuccessResponse(logs);
        }
    }
}
