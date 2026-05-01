using FluentValidation;
using SchoolDMS.API.Models.DTOs.Visits;

namespace SchoolDMS.API.Validators
{
    public class CreateVisitDTOValidator : AbstractValidator<CreateVisitDTO>
    {
        public CreateVisitDTOValidator()
        {
            RuleFor(x => x.SchoolId).GreaterThan(0);
            RuleFor(x => x.ProjectId).GreaterThan(0);
            RuleFor(x => x.VisitType).IsInEnum();
            RuleFor(x => x.VisitDate).NotEmpty().LessThanOrEqualTo(DateTime.UtcNow.AddDays(1)).WithMessage("Visit date cannot be in the future.");
        }
    }

    public class VisitApprovalDTOValidator : AbstractValidator<VisitApprovalDTO>
    {
        public VisitApprovalDTOValidator()
        {
            RuleFor(x => x.RejectionReasons).NotEmpty().When(x => !x.IsApproved).WithMessage("Rejection reason is required when rejecting a visit.");
        }
    }
}
