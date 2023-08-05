using Application.Dtos;
using FluentValidation;

namespace Application.Validations;
public class CreateProductValidation : AbstractValidator<CreateProductDto>
{
	public CreateProductValidation()
	{
		RuleFor(x => Convert.ToDouble(x.Price)).GreaterThan(1.0).WithMessage("Price can not be less than 1");

		RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name can not be empty or null");

		RuleFor(x => x.RestaurantId).NotNull().NotEmpty().WithMessage("Restaurant can not be null or empty");
	}
}
