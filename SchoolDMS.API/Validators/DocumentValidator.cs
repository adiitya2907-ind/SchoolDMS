using FluentValidation;
using SchoolDMS.API.Models.DTOs.Documents;

namespace SchoolDMS.API.Validators
{
    public class DocumentUploadDTOValidator : AbstractValidator<DocumentUploadDTO>
    {
        public DocumentUploadDTOValidator()
        {
            RuleFor(x => x.VisitId).GreaterThan(0);
            RuleFor(x => x.DocumentType).IsInEnum();
            RuleFor(x => x.File).NotNull().WithMessage("File is required.");
        }
    }
}
