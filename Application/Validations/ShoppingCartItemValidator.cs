using Application.Dtos;
using FluentValidation;

namespace Application.Validations;
public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItemDto>
{
	public ShoppingCartItemValidator()
	{
		RuleFor(x => Convert.ToDouble(x.Price)).GreaterThan(1.0).WithMessage("Price can not be less than 1");

		RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage("Quantity can not be less than 1");

		RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name can not be null or empty");

		RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id can not be null or empty");
	}
}
