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
using FluentValidation;
using InvestmentPortfolio.Application.Commands.Product;
using Microsoft.Extensions.Caching.Distributed;
using InvestmentPortfolio.Application.Validators.Product;

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

        #region Cache
        //var cacheConnection = configuration.GetConnectionString("CacheConnection");

        //var cachingSettings = configuration.GetSection("CachingSettings");
        //var absoluteExpirationRelativeToNow = cachingSettings.GetValue<int>("AbsoluteExpirationRelativeToNow");
        //var slidingExpiration = cachingSettings.GetValue<int>("SlidingExpiration");

        //services.AddStackExchangeRedisCache(o =>
        //{
        //    o.Configuration = cacheConnection;
        //});

        //var cacheEntryOptions = new DistributedCacheEntryOptions
        //{
        //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(absoluteExpirationRelativeToNow),
        //    SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration)
        //};

        //services.AddSingleton<DistributedCacheEntryOptions>(cacheEntryOptions);
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
