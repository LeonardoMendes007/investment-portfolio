using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Interfaces.Repositories;

namespace InvestmentPortfolio.Infra.Persistence.Repositories;
public class InvestmentRepository : IInvestmentRepository
{
    private readonly InvestimentPortfolioDbContext _investimentPortfolioDbContext;
    public InvestmentRepository(InvestimentPortfolioDbContext movieAppDbContext)
    {
        _investimentPortfolioDbContext = movieAppDbContext;
    }

    public async Task SaveAsync(Investment investment)
    {
       await _investimentPortfolioDbContext.Investiments.AddAsync(investment);
    }
}
