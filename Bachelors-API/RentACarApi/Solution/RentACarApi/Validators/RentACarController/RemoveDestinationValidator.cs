using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class RemoveDestinationValidator : AbstractValidator<RemoveDestination>
    {
        public RemoveDestinationValidator()
        {
            RuleFor(x => x.Location).NotNull().MinimumLength(2).MaximumLength(30);
        }
    }
}
