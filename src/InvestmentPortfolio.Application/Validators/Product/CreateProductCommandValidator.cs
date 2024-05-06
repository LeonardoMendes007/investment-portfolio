using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;

namespace InvestmentPortfolio.Application.Validators.Product;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        RuleFor(x => x.Price).GreaterThan(0).ScalePrecision(2, 7);
        RuleFor(x => x.ExpirationDate).NotNull().GreaterThan(DateTime.Now);
    }
}
