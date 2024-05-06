using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface ITransactionService
{
    Task<Transaction> Create(Transaction transaction);
    
}
