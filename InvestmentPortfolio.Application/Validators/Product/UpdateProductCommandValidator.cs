using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;

namespace InvestmentPortfolio.Application.Validators.Product;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(300);
        RuleFor(x => x.InitialPrice).GreaterThan(0).ScalePrecision(2, 7);
        RuleFor(x => x.CurrentPrice).GreaterThan(0).ScalePrecision(2, 7);
        RuleFor(x => x.ExpirationDate).NotNull().GreaterThan(DateTime.Now);
    }
}
