using InvestmentPortfolio.Domain.Interfaces.Repositories;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using InvestmentPortfolio.Infra.Persistence.Repositories;

namespace InvestmentPortfolio.Infra.Persistence.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private IProductRepository _ProductRepository;
    private ITransactionRepository _transactionRepository;
    private IInvestmentRepository _investmentRepository;
    private ICustomerRepository _customerRepository;

    private readonly InvestimentPortfolioDbContext _investimentPortfolioDbContext;

    public UnitOfWork(InvestimentPortfolioDbContext investimentPortfolioDbContext)
    {
        _investimentPortfolioDbContext = investimentPortfolioDbContext;
    }

    public IProductRepository ProductRepository
    {
        get
        {
            return _ProductRepository = _ProductRepository ?? new ProductRepository(_investimentPortfolioDbContext);
        }
    }
    public ITransactionRepository TransactionRepository
    {
        get
        {
            return _transactionRepository = _transactionRepository ?? new TransactionRepository(_investimentPortfolioDbContext);
        }
    }

    public IInvestmentRepository InvestmentRepository
    {
        get
        {
            return _investmentRepository = _investmentRepository ?? new InvestmentRepository(_investimentPortfolioDbContext);
        }
    }

    public ICustomerRepository CustomerRepository
    {
        get
        {
            return _customerRepository = _customerRepository ?? new CustomerRepository(_investimentPortfolioDbContext);
        }
    }

    public async Task CommitAsync()
    {
        await _investimentPortfolioDbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _investimentPortfolioDbContext.Dispose();
    }
}
