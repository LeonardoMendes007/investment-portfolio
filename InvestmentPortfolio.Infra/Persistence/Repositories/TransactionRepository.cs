using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Interfaces.Repositories;

namespace InvestmentPortfolio.Infra.Persistence.Repositories;
public class TransactionRepository : ITransactionRepository
{
    private readonly InvestimentPortfolioDbContext _investimentPortfolioDbContext;
    public TransactionRepository(InvestimentPortfolioDbContext movieAppDbContext)
    {
        _investimentPortfolioDbContext = movieAppDbContext;
    }
    public async Task SaveAsync(Transaction transaction)
    {
        await _investimentPortfolioDbContext.Transactions.AddAsync(transaction);
    }
}
