using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services;
public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Transaction> Create(Transaction transaction)
    {
        var customer = await _unitOfWork.CustomerRepository.FindByIdAsync(transaction.CustomerId);
        
        if (customer is null)
        {
            throw new ResourceNotFoundException(transaction.CustomerId, $"There is no customer with id = {transaction.CustomerId}");
        }

        var product = await _unitOfWork.ProductRepository.FindByIdAsync(transaction.ProductId);
        
        if (product is null)
        {
            throw new ResourceNotFoundException(transaction.ProductId, $"There is no product with id = {transaction.ProductId}");
        }

        transaction.Id = Guid.NewGuid();
        transaction.PU = product.CurrentPrice;
        transaction.Customer = customer;
        transaction.Product = product;

        await _unitOfWork.TransactionRepository.SaveAsync(transaction);

        return transaction;
    }

}
