using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Mapper.AutoMapperConfig;
using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Validators.Product;
using InvestmentPortfolio.Domain.Interfaces.Repositories;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using InvestmentPortfolio.Infra.Persistence;
using InvestmentPortfolio.Infra.Persistence.Repositories;
using InvestmentPortfolio.Infra.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentPortfolio.CrossCutting.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Banco de dados
        services.AddDbContext<InvestimentPortfolioDbContext>(options =>
           options.UseInMemoryDatabase(configuration.GetConnectionString("InvestimentPortfolioDbConnection")));
        #endregion

        #region MediatR
        var assembly = AppDomain.CurrentDomain.Load("InvestmentPortfolio.Application");

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        #endregion

        #region Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IInvestmentService, InvestmentService>();
        services.AddScoped<ITransactionService, TransactionService>();
        #endregion

        #region Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IInvestmentRepository, InvestmentRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Validators
        services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        services.AddTransient<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
        #endregion


        return services;
    }
}
