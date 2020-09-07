using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class ChangeVehicleLocationValidator : AbstractValidator<ChangeVehicleLocation>
    {
        public ChangeVehicleLocationValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().GreaterThan(0);
            RuleFor(x => x.NewCity).NotNull().MinimumLength(2).MaximumLength(30);
        }
    }
}
