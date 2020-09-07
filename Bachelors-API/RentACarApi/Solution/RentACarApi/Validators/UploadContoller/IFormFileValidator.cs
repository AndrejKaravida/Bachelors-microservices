using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace RentACarApi.Validators
{
    public class IFormFileValidator : AbstractValidator<IFormFile>
    {
        public IFormFileValidator()
        {
            RuleFor(x => x.Length).NotNull().LessThanOrEqualTo(500);
        }
    }
}
