using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IVisitService
    {
        Task<PaginatedResponse<VisitListDTO>> GetVisitsAsync(int? engineerId = null, int? schoolId = null, int? statusId = null, int pageNumber = 1, int pageSize = 20);
        Task<ApiResponse<VisitDTO>> GetVisitByIdAsync(int visitId);
        Task<ApiResponse<int>> CreateVisitAsync(CreateVisitDTO request, int engineerId);
        Task<ApiResponse<bool>> UpdateVisitAsync(int visitId, CreateVisitDTO request, int engineerId);
        Task<ApiResponse<bool>> DeleteVisitAsync(int visitId, int engineerId);
        Task<ApiResponse<IEnumerable<VisitListDTO>>> GetPendingVerificationsAsync();
        
        // Actions
        Task<ApiResponse<bool>> CheckInAsync(int visitId, int engineerId, decimal latitude, decimal longitude);
        Task<ApiResponse<bool>> CheckOutAsync(int visitId, int engineerId);
        Task<ApiResponse<bool>> SubmitVisitAsync(int visitId, int engineerId);
    }
}
