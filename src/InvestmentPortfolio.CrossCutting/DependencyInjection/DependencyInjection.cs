using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Mapper.AutoMapperConfig;
using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Application.Validators.Product;
using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Interfaces.Repositories;
using InvestmentPortfolio.Domain.Interfaces.Services;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using InvestmentPortfolio.Infra.Persistence;
using InvestmentPortfolio.Infra.Persistence.Data;
using InvestmentPortfolio.Infra.Persistence.Repositories;
using InvestmentPortfolio.Infra.Persistence.UnitOfWork;
using InvestmentPortfolio.Job;
using InvestmentPortfolio.Job.Options;
using InvestmentPortfolio.Job.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace InvestmentPortfolio.CrossCutting.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        #region Banco de dados
        services.AddDbContext<InvestimentPortfolioDbContext>(options =>
           options.UseInMemoryDatabase("InvestimentPortfolioDbConnection"));
        #endregion

        #region MediatR
        var assembly = AppDomain.CurrentDomain.Load("InvestmentPortfolio.Application");

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        #endregion

        #region AutoMapper
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        #endregion

        #region EmailOptions

        var emailOptions = configuration.GetSection("EmailSettings").Get<EmailOptions>();
        services.AddSingleton<EmailOptions>(emailOptions);

        #endregion

        #region WorkerOptions

        var workerOptions = configuration.GetSection("WorkerSettings").Get<WorkerOptions>();
        services.AddSingleton<WorkerOptions>(workerOptions);

        #endregion

        #region Services
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IWorkerService, WorkerService>();
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

        #region Workers
        services.AddHostedService<InfoWorker>();
        #endregion

        #region InitializeDb
        DbInitializer.Seed(services.BuildServiceProvider());
        #endregion

        return services;
    }
}
