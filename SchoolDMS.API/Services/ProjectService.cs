using AutoMapper;
using SchoolDMS.API.Models.DTOs.Projects;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ProjectDTO>>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return ApiResponse<IEnumerable<ProjectDTO>>.SuccessResponse(_mapper.Map<IEnumerable<ProjectDTO>>(projects));
        }

        public async Task<ApiResponse<ProjectDTO>> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return ApiResponse<ProjectDTO>.FailureResponse("Project not found", 404);

            return ApiResponse<ProjectDTO>.SuccessResponse(_mapper.Map<ProjectDTO>(project));
        }

        public async Task<ApiResponse<int>> CreateProjectAsync(CreateProjectDTO request)
        {
            var project = _mapper.Map<Project>(request);
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(project.ProjectId, "Project created", 201);
        }

        public async Task<ApiResponse<bool>> UpdateProjectAsync(int id, CreateProjectDTO request)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return ApiResponse<bool>.FailureResponse("Project not found", 404);

            _mapper.Map(request, project);
            _projectRepository.Update(project);
            await _projectRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Project updated");
        }

        public async Task<ApiResponse<bool>> DeleteProjectAsync(int id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            if (project == null) return ApiResponse<bool>.FailureResponse("Project not found", 404);

            _projectRepository.Remove(project);
            await _projectRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Project deleted");
        }
    }
}
