using SchoolDMS.API.Models.DTOs.Projects;
using SchoolDMS.API.Models.Responses;

namespace SchoolDMS.API.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<ProjectDTO>>> GetAllProjectsAsync();
        Task<ApiResponse<ProjectDTO>> GetProjectByIdAsync(int id);
        Task<ApiResponse<int>> CreateProjectAsync(CreateProjectDTO request);
        Task<ApiResponse<bool>> UpdateProjectAsync(int id, CreateProjectDTO request);
        Task<ApiResponse<bool>> DeleteProjectAsync(int id);
    }
}
