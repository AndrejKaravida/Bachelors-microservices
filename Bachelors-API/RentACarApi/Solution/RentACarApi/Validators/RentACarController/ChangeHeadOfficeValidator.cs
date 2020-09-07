using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class ChangeHeadOfficeValidator : AbstractValidator<ChangeHeadOffice>
    {
        public ChangeHeadOfficeValidator()
        {
            RuleFor(x => x.HeadOffice).NotNull().MinimumLength(2).MaximumLength(50);
        }
    }
}
