using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IApprovalService
    {
        Task<ApiResponse<bool>> ProcessApprovalAsync(int visitId, int verifierId, VisitApprovalDTO request);
        Task<ApiResponse<IEnumerable<ApprovalWorkflow>>> GetApprovalHistoryAsync(int visitId);
    }
}
