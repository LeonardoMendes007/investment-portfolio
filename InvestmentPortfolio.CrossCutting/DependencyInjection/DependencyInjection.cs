using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvestmentPortfolio.Infra.Persistence;
using InvestmentPortfolio.Application.Mapper.AutoMapperConfig;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using InvestmentPortfolio.Infra.Persistence.UnitOfWork;
using InvestmentPortfolio.Domain.Interfaces.Repositories;
using InvestmentPortfolio.Infra.Persistence.Repositories;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Services;

namespace InvestmentPortfolio.CrossCutting.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Banco de dados
        services.AddDbContext<InvestimentPortfolioDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("InvestimentPortfolioDbConnection")));
        #endregion

        #region MediatR
        var assembly = AppDomain.CurrentDomain.Load("InvestmentPortfolio.Application");

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        #endregion

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IInvestmentRepository, InvestmentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Repositories
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IInvestmentService, InvestmentService>();
        services.AddScoped<ITransactionService, TransactionService>();
        #endregion

        return services;
    }
}
