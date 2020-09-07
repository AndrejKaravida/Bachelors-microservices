using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class BranchToAddValidator : AbstractValidator<BranchToAdd>
    {
        public BranchToAddValidator()
        {
            RuleFor(x => x.Address).NotEmpty().MinimumLength(5).MaximumLength(50);
            RuleFor(x => x.City).NotEmpty().MinimumLength(2).MaximumLength(30);
            RuleFor(x => x.Country).NotEmpty().MinimumLength(2).MaximumLength(30);
        }
    }
}
