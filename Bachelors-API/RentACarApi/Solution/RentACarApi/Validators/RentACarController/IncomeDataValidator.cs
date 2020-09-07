using FluentValidation;
using RentACarApi.Dtos;

namespace RentACarApi.Validators
{
    public class IncomeDataValidator : AbstractValidator<IncomeData>
    {
        public IncomeDataValidator()
        {
            RuleFor(x => x.StartingDate).NotNull();
            RuleFor(x => x.FinalDate).NotNull();
        }
    }
}
