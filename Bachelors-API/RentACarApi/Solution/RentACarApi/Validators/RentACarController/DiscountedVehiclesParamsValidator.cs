using FluentValidation;
using RentACarApi.Data;

namespace RentACarApi.Validators.RentACarController
{
    public class DiscountedVehiclesParamsValidator : AbstractValidator<DiscountedVehiclesParams>
    {
        public DiscountedVehiclesParamsValidator()
        {
            RuleFor(x => x.numberOfDays).NotNull().GreaterThan(0);
            RuleFor(x => x.pickupLocation).NotNull().MinimumLength(2);
            RuleFor(x => x.startingDate).NotNull().MinimumLength(2);
        }
    }
}
