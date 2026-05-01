using AutoMapper;
using SchoolDMS.API.Models.DTOs.Schools;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IRepository<School> _schoolRepository;
        private readonly IMapper _mapper;

        public SchoolService(IRepository<School> schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<SchoolDTO>> GetAllSchoolsAsync(int pageNumber = 1, int pageSize = 20)
        {
            var schools = await _schoolRepository.GetAllAsync();
            var totalRecords = schools.Count();
            
            var paginatedSchools = schools.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var schoolDtos = _mapper.Map<IEnumerable<SchoolDTO>>(paginatedSchools);

            return PaginatedResponse<SchoolDTO>.SuccessResponse(schoolDtos, pageNumber, pageSize, totalRecords);
        }

        public async Task<ApiResponse<SchoolDTO>> GetSchoolByIdAsync(int id)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                return ApiResponse<SchoolDTO>.FailureResponse("School not found", 404);
            }

            return ApiResponse<SchoolDTO>.SuccessResponse(_mapper.Map<SchoolDTO>(school));
        }

        public async Task<ApiResponse<IEnumerable<SchoolDTO>>> SearchSchoolsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return ApiResponse<IEnumerable<SchoolDTO>>.SuccessResponse(Enumerable.Empty<SchoolDTO>());
            }

            var term = searchTerm.ToLower();
            var schools = await _schoolRepository.FindAsync(s => 
                s.UdiseCode.ToLower().Contains(term) || 
                s.SchoolName.ToLower().Contains(term) || 
                s.District.ToLower().Contains(term));

            return ApiResponse<IEnumerable<SchoolDTO>>.SuccessResponse(_mapper.Map<IEnumerable<SchoolDTO>>(schools));
        }

        public async Task<ApiResponse<int>> CreateSchoolAsync(CreateSchoolDTO request)
        {
            if (await _schoolRepository.ExistsAsync(s => s.UdiseCode.ToLower() == request.UdiseCode.ToLower()))
            {
                return ApiResponse<int>.FailureResponse("School with this UDISE code already exists", 409);
            }

            var school = _mapper.Map<School>(request);
            await _schoolRepository.AddAsync(school);
            await _schoolRepository.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(school.SchoolId, "School created successfully", 201);
        }

        public async Task<ApiResponse<bool>> UpdateSchoolAsync(int id, UpdateSchoolDTO request)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                return ApiResponse<bool>.FailureResponse("School not found", 404);
            }

            _mapper.Map(request, school);
            _schoolRepository.Update(school);
            await _schoolRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "School updated successfully");
        }

        public async Task<ApiResponse<bool>> DeleteSchoolAsync(int id)
        {
            var school = await _schoolRepository.GetByIdAsync(id);
            if (school == null)
            {
                return ApiResponse<bool>.FailureResponse("School not found", 404);
            }

            _schoolRepository.Remove(school);
            await _schoolRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "School deleted successfully");
        }
    }
}
