using AutoMapper;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services;
public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Investment> GetInvestmentByProductAsync(Guid id, Guid productId)
    {
        var investment = await _unitOfWork.CustomerRepository.FindInvestimentByProductIdAsync(id, productId);

        if(investment is null)
        {
            throw new ResourceNotFoundException(productId);
        }

        return investment;
    }

    public async Task<IQueryable<Investment>> GetInvestmentByQueryAsync(Guid id)
    {
        if (await _unitOfWork.CustomerRepository.FindByIdAsync(id) is null)
        {
            throw new ResourceNotFoundException(id);
        }

        return _unitOfWork.CustomerRepository.FindAllInvestments(id);
    }

    public async Task<IQueryable<Transaction>> GetTransactionsByProductAsync(Guid id, Guid productId)
    {
        if (await _unitOfWork.CustomerRepository.FindByIdAsync(id) is null)
        {
            throw new ResourceNotFoundException(id);
        }

        if (await _unitOfWork.ProductRepository.FindByIdAsync(productId) is null)
        {
            throw new ResourceNotFoundException(id);
        }

        var transactions = _unitOfWork.CustomerRepository.FindTransactionsByProductId(id, productId);

        return transactions;
    }

    public async Task<IQueryable<Transaction>> GetTransactionByQueryAsync(Guid id)
    {
        if (await _unitOfWork.CustomerRepository.FindByIdAsync(id) is null)
        {
            throw new ResourceNotFoundException(id);
        }

        var transactions = _unitOfWork.CustomerRepository.FindAllTransactions(id);

        return transactions;
    }
}
