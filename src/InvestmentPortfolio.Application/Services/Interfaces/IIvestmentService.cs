using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services.Interfaces;
public interface IInvestmentService
{
    Task<Investment> BuyInvestment(Transaction transaction);
    Task<Investment> SellInvestment(Transaction transaction);
    
}
