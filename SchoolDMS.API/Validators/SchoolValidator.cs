using FluentValidation;
using SchoolDMS.API.Models.DTOs.Schools;

namespace SchoolDMS.API.Validators
{
    public class CreateSchoolDTOValidator : AbstractValidator<CreateSchoolDTO>
    {
        public CreateSchoolDTOValidator()
        {
            RuleFor(x => x.UdiseCode).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SchoolName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.District).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Block).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);
        }
    }

    public class UpdateSchoolDTOValidator : AbstractValidator<UpdateSchoolDTO>
    {
        public UpdateSchoolDTOValidator()
        {
            RuleFor(x => x.SchoolName).NotEmpty().MaximumLength(255);
            RuleFor(x => x.District).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Block).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);
        }
    }
}
