using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class RateDataValidator : AbstractValidator<RateData>
    {
        public RateDataValidator()
        {
            RuleFor(x => x.UserId).NotNull().MinimumLength(1).MaximumLength(50);
            RuleFor(x => x.VehicleId).NotNull().GreaterThan(0);
            RuleFor(x => x.CompanyId).NotNull().GreaterThan(0);
            RuleFor(x => x.VehicleRating).NotNull();
            RuleFor(x => x.CompanyRating).NotNull();
            
        }
    }
}
