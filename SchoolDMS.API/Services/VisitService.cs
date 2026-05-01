using AutoMapper;
using SchoolDMS.API.Helpers;
using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Enums;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class VisitService : IVisitService
    {
        private readonly IVisitRepository _visitRepository;
        private readonly IRepository<School> _schoolRepository;
        private readonly IMapper _mapper;

        public VisitService(IVisitRepository visitRepository, IRepository<School> schoolRepository, IMapper mapper)
        {
            _visitRepository = visitRepository;
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<VisitListDTO>> GetVisitsAsync(int? engineerId = null, int? schoolId = null, int? statusId = null, int pageNumber = 1, int pageSize = 20)
        {
            var visits = await _visitRepository.GetVisitsWithDetailsAsync(engineerId, schoolId, statusId);
            var totalRecords = visits.Count();
            
            var paginatedVisits = visits.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var visitDtos = _mapper.Map<IEnumerable<VisitListDTO>>(paginatedVisits);

            return PaginatedResponse<VisitListDTO>.SuccessResponse(visitDtos, pageNumber, pageSize, totalRecords);
        }

        public async Task<ApiResponse<VisitDTO>> GetVisitByIdAsync(int visitId)
        {
            var visit = await _visitRepository.GetVisitWithDetailsAsync(visitId);
            if (visit == null) return ApiResponse<VisitDTO>.FailureResponse("Visit not found", 404);

            return ApiResponse<VisitDTO>.SuccessResponse(_mapper.Map<VisitDTO>(visit));
        }

        public async Task<ApiResponse<int>> CreateVisitAsync(CreateVisitDTO request, int engineerId)
        {
            if (request.VisitDate.Date > DateTime.UtcNow.Date)
            {
                return ApiResponse<int>.FailureResponse("Cannot create visit for future dates", 400);
            }

            var visit = _mapper.Map<Visit>(request);
            visit.EngineerId = engineerId;
            visit.Status = VisitStatusEnum.Draft;
            visit.CreatedAt = DateTime.UtcNow;

            await _visitRepository.AddAsync(visit);
            await _visitRepository.SaveChangesAsync();

            return ApiResponse<int>.SuccessResponse(visit.VisitId, "Visit created successfully", 201);
        }

        public async Task<ApiResponse<bool>> UpdateVisitAsync(int visitId, CreateVisitDTO request, int engineerId)
        {
            var visit = await _visitRepository.GetByIdAsync(visitId);
            if (visit == null || visit.EngineerId != engineerId) 
                return ApiResponse<bool>.FailureResponse("Visit not found or unauthorized", 404);

            if (visit.Status != VisitStatusEnum.Draft)
                return ApiResponse<bool>.FailureResponse("Only draft visits can be updated", 400);

            _mapper.Map(request, visit);
            visit.UpdatedAt = DateTime.UtcNow;

            _visitRepository.Update(visit);
            await _visitRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Visit updated");
        }

        public async Task<ApiResponse<bool>> DeleteVisitAsync(int visitId, int engineerId)
        {
            var visit = await _visitRepository.GetByIdAsync(visitId);
            if (visit == null || visit.EngineerId != engineerId) 
                return ApiResponse<bool>.FailureResponse("Visit not found or unauthorized", 404);

            if (visit.Status != VisitStatusEnum.Draft)
                return ApiResponse<bool>.FailureResponse("Only draft visits can be deleted", 400);

            _visitRepository.Remove(visit);
            await _visitRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Visit deleted");
        }

        public async Task<ApiResponse<IEnumerable<VisitListDTO>>> GetPendingVerificationsAsync()
        {
            var visits = await _visitRepository.GetPendingVerificationsAsync();
            return ApiResponse<IEnumerable<VisitListDTO>>.SuccessResponse(_mapper.Map<IEnumerable<VisitListDTO>>(visits));
        }

        public async Task<ApiResponse<bool>> CheckInAsync(int visitId, int engineerId, decimal latitude, decimal longitude)
        {
            var visit = await _visitRepository.GetVisitWithDetailsAsync(visitId);
            if (visit == null || visit.EngineerId != engineerId) 
                return ApiResponse<bool>.FailureResponse("Visit not found", 404);

            if (visit.Status != VisitStatusEnum.Draft)
                return ApiResponse<bool>.FailureResponse("Can only check-in to draft visits", 400);

            if (visit.CheckInTime.HasValue)
                return ApiResponse<bool>.FailureResponse("Already checked in", 400);

            var school = visit.School;
            if (school?.Latitude == null || school?.Longitude == null)
            {
                // Accept if school has no GPS
                visit.IsGpsVerified = null;
            }
            else
            {
                // Validate 500m tolerance
                visit.IsGpsVerified = GeoLocationHelper.IsWithinTolerance(
                    (double)school.Latitude.Value, (double)school.Longitude.Value,
                    (double)latitude, (double)longitude, 500);
            }

            visit.CheckInTime = DateTime.UtcNow;
            visit.GpsLatitude = latitude;
            visit.GpsLongitude = longitude;
            visit.UpdatedAt = DateTime.UtcNow;

            _visitRepository.Update(visit);
            await _visitRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Checked in successfully");
        }

        public async Task<ApiResponse<bool>> CheckOutAsync(int visitId, int engineerId)
        {
            var visit = await _visitRepository.GetByIdAsync(visitId);
            if (visit == null || visit.EngineerId != engineerId) 
                return ApiResponse<bool>.FailureResponse("Visit not found", 404);

            if (!visit.CheckInTime.HasValue)
                return ApiResponse<bool>.FailureResponse("Must check-in first", 400);

            if (visit.CheckOutTime.HasValue)
                return ApiResponse<bool>.FailureResponse("Already checked out", 400);

            visit.CheckOutTime = DateTime.UtcNow;
            visit.UpdatedAt = DateTime.UtcNow;

            _visitRepository.Update(visit);
            await _visitRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Checked out successfully");
        }

        public async Task<ApiResponse<bool>> SubmitVisitAsync(int visitId, int engineerId)
        {
            var visit = await _visitRepository.GetVisitWithDetailsAsync(visitId);
            if (visit == null || visit.EngineerId != engineerId) 
                return ApiResponse<bool>.FailureResponse("Visit not found", 404);

            if (visit.Status != VisitStatusEnum.Draft)
                return ApiResponse<bool>.FailureResponse("Only draft visits can be submitted", 400);

            if (!visit.CheckInTime.HasValue || !visit.CheckOutTime.HasValue)
                return ApiResponse<bool>.FailureResponse("Check-in and Check-out are mandatory before submission", 400);

            if (visit.CheckInTime > visit.CheckOutTime)
                return ApiResponse<bool>.FailureResponse("Check-in time cannot be after check-out time", 400);

            if (!ValidationHelper.HasAllMandatoryDocuments(visit))
                return ApiResponse<bool>.FailureResponse("All mandatory documents must be uploaded before submission", 400);

            visit.Status = VisitStatusEnum.Submitted;
            // Workflow transitions immediately to PendingVerification
            visit.Status = VisitStatusEnum.PendingVerification; 
            visit.UpdatedAt = DateTime.UtcNow;

            _visitRepository.Update(visit);
            await _visitRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Visit submitted for verification");
        }
    }
}
