using FluentValidation;
using AuthApi.Dtos;

namespace AuthApi.Validators.AuthController
{
    public class EmailDtoValidator : AbstractValidator<EmailDto>
    {
        public EmailDtoValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
