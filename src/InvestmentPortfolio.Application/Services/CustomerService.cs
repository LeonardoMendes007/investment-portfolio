using AutoMapper;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services;
public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<InvestmentDetails> GetInvestmentByProductAsync(Guid id, Guid productId)
    {
        var investment = await _unitOfWork.CustomerRepository.FindInvestimentByProductIdAsync(id, productId);

        if(investment is null)
        {
            throw new ResourceNotFoundException(productId);
        }

        return _mapper.Map<InvestmentDetails>(investment);
    }

    public async Task<IQueryable<InvestmentSummary>> GetInvestmentByQueryAsync(Guid id)
    {
        if (await _unitOfWork.CustomerRepository.FindByIdAsync(id) is null)
        {
            throw new ResourceNotFoundException(id);
        }

        var investments = _unitOfWork.CustomerRepository.FindAllInvestments(id);

        var investmentsSummary = investments
            .Select(t => new InvestmentSummary
            {
                ProductId = t.ProductId,
                ProductName = t.Product.Name,
                Quantity = t.Quantity,
                InvestmentAmount = t.InvestmentAmount,
                CurrentAmount = t.CurrentAmount
            });

        return investmentsSummary;
    }

    public async Task<IQueryable<TransactionDetails>> GetTransactionsByProductAsync(Guid id, Guid productId)
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

        var transactionsDetails = transactions
            .Select(t => new TransactionDetails
            {
                Id = t.Id,
                ProductId = t.ProductId,
                CustomerId = t.CustomerId,
                ProductName = t.Product.Name,
                PU = t.PU,
                Quantity = t.Quantity,
                TransactionType = t.TransactionType,
                TransactionTypeName = t.TransactionType == 0 ? "Buy" : "Sell",
                Date = t.Date
            });

        return transactionsDetails;
    }

    public async Task<IQueryable<TransactionSummary>> GetTransactionByQueryAsync(Guid id)
    {
        if (await _unitOfWork.CustomerRepository.FindByIdAsync(id) is null)
        {
            throw new ResourceNotFoundException(id);
        }

        var transactions = _unitOfWork.CustomerRepository.FindAllTransactions(id);

        var transactionsSummary = transactions
            .Select(t => new TransactionSummary
            {
                Id = t.Id,
                ProductId = t.ProductId,
                CustomerId = t.CustomerId,
                ProductName = t.Product.Name,
                TransactionType = t.TransactionType,
                TransactionTypeName = t.TransactionType == 0 ? "Buy" : "Sell",
                Amount = t.PU * t.Quantity,
                Quantity = t.Quantity,
                Date = t.Date
            });

        return transactionsSummary;
    }
}
