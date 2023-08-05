using Application.Dtos;
using FluentValidation;

namespace Application.Validations;
public class AddressValidator : AbstractValidator<AddressDto>
{
	public AddressValidator()
	{
		RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First name can not be null or empty");

        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name can not be null or empty");

        RuleFor(x => x.Street).NotNull().NotEmpty().WithMessage("Street can not be null or empty");

        RuleFor(x => x.State).NotNull().NotEmpty().WithMessage("State name can not be null or empty");

        RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City name can not be null or empty");
    }
}
