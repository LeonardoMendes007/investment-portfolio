using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services;
public class InvestmentService : IInvestmentService
{
    private readonly IUnitOfWork _unitOfWork;

    public InvestmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Investment> BuyInvestmentAsync(Transaction transaction)
    {
        if (!transaction.Product.IsActive)
        {
            throw new ProductIsInativeException(transaction.ProductId);
        }

        if (transaction.Product.ExpirationDate < DateTime.Now)
        {
            throw new ProductIsInativeException(transaction.ProductId);
        }

        transaction.Customer.Debit((transaction.Product.CurrentPrice * transaction.Quantity));

        var investment = await _unitOfWork.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId);

        if (investment is null)
        {
            investment = new Investment()
            {
                Product = transaction.Product,
                Customer = transaction.Customer
            };

            await _unitOfWork.InvestmentRepository.SaveAsync(investment);
        }

        investment.Quantity += transaction.Quantity;
        investment.InvestmentAmount += transaction.Quantity * transaction.PU;

        return investment;
    }

    public async Task<Investment> SellInvestmentAsync(Transaction transaction)
    {
        var investment = await _unitOfWork.CustomerRepository.FindInvestimentByProductIdAsync(transaction.CustomerId, transaction.ProductId);

        if (investment is null)
        {
            throw new InvalidOperationException($"The customer does not own the product with id = {transaction.ProductId}");
        }

        if (investment.Quantity < transaction.Quantity)
        {
            throw new InvalidOperationException($"The quantity sold is greater than the customer has");
        }

        investment.Quantity -= transaction.Quantity;
        investment.InvestmentAmount -= transaction.Quantity * transaction.PU;

        transaction.Customer.Credit((transaction.Product.CurrentPrice * transaction.Quantity));

        return investment;
    }
}
