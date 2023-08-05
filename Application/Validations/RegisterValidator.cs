using Application.Dtos;
using FluentValidation;

namespace Application.Validations;
internal class RegisterValidator : AbstractValidator<RegisterDto>
{
	public RegisterValidator()
	{
        RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email can not be null or empty");

        RuleFor(x => x.Email).EmailAddress().WithMessage("Specify a valid email address");

        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Password can not be null or empty");

        RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 characters");
    }
}
