using SchoolDMS.API.Models.DTOs.Schools;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface ISchoolService
    {
        Task<PaginatedResponse<SchoolDTO>> GetAllSchoolsAsync(int pageNumber = 1, int pageSize = 20);
        Task<ApiResponse<SchoolDTO>> GetSchoolByIdAsync(int id);
        Task<ApiResponse<IEnumerable<SchoolDTO>>> SearchSchoolsAsync(string searchTerm);
        Task<ApiResponse<int>> CreateSchoolAsync(CreateSchoolDTO request);
        Task<ApiResponse<bool>> UpdateSchoolAsync(int id, UpdateSchoolDTO request);
        Task<ApiResponse<bool>> DeleteSchoolAsync(int id);
    }
}
