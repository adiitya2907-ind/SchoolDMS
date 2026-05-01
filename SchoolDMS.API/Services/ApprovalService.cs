using SchoolDMS.API.Models.DTOs.Visits;
using SchoolDMS.API.Models.Entities;
using SchoolDMS.API.Models.Enums;
using SchoolDMS.API.Models.Responses;
using SchoolDMS.API.Repositories.Interfaces;
using SchoolDMS.API.Services.Interfaces;

namespace SchoolDMS.API.Services
{
    public class ApprovalService : IApprovalService
    {
        private readonly IApprovalRepository _approvalRepository;
        private readonly IVisitRepository _visitRepository;

        public ApprovalService(IApprovalRepository approvalRepository, IVisitRepository visitRepository)
        {
            _approvalRepository = approvalRepository;
            _visitRepository = visitRepository;
        }

        public async Task<ApiResponse<bool>> ProcessApprovalAsync(int visitId, int verifierId, VisitApprovalDTO request)
        {
            var visit = await _visitRepository.GetVisitWithDetailsAsync(visitId);
            if (visit == null) return ApiResponse<bool>.FailureResponse("Visit not found", 404);

            if (visit.Status != VisitStatusEnum.PendingVerification)
                return ApiResponse<bool>.FailureResponse("Visit is not pending verification", 400);

            if (visit.EngineerId == verifierId)
                return ApiResponse<bool>.FailureResponse("Verifiers cannot approve their own visits", 403);

            var workflow = new ApprovalWorkflow
            {
                VisitId = visitId,
                VerifierId = verifierId,
                ApprovalStatus = request.IsApproved ? ApprovalStatusEnum.Approved : ApprovalStatusEnum.Rejected,
                Comments = request.Comments,
                RejectionReasons = request.IsApproved ? null : request.RejectionReasons,
                ApprovedAt = request.IsApproved ? DateTime.UtcNow : null,
                CreatedAt = DateTime.UtcNow
            };

            await _approvalRepository.AddAsync(workflow);

            visit.Status = request.IsApproved ? VisitStatusEnum.Approved : VisitStatusEnum.Rejected;
            if (!request.IsApproved)
            {
                visit.RejectionReason = request.RejectionReasons;
                // Important: Rejected visits should be editable by engineer again
                visit.Status = VisitStatusEnum.Draft;
            }

            visit.UpdatedAt = DateTime.UtcNow;
            _visitRepository.Update(visit);

            await _approvalRepository.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResponse(true, "Visit approval processed");
        }

        public async Task<ApiResponse<IEnumerable<ApprovalWorkflow>>> GetApprovalHistoryAsync(int visitId)
        {
            var history = await _approvalRepository.GetHistoryByVisitIdAsync(visitId);
            return ApiResponse<IEnumerable<ApprovalWorkflow>>.SuccessResponse(history);
        }
    }
}
