using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;

namespace InvestmentPortfolio.Infra.Persistence;
public sealed class InvestimentPortfolioDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Investment> Investiments { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    
    public InvestimentPortfolioDbContext(DbContextOptions<InvestimentPortfolioDbContext> options)
    : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(InvestimentPortfolioDbContext)
            .Assembly);
    }
}
