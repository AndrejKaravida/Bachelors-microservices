using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class DeleteVehicleValidator : AbstractValidator<DeleteVehicle>
    {
        public DeleteVehicleValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().GreaterThan(0);
        }
    }
}
