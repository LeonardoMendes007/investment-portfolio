using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentPortfolio.Infra.Persistence.Data;
public static class DbInitializer 
{
    public static void Seed(IServiceProvider provider)
    {
        var dbContext = provider.GetService<InvestimentPortfolioDbContext>();

        dbContext.Customers.AddRange(new List<Customer>
        {
            new Customer{
                Id = Guid.Parse("FE232D84-BE96-4669-954C-215B65F6DBE4"),
                Name = "Leonardo",
                Address = "Av. Itaquera",
                Balance = 1000
            },
            new Customer{
                Id = Guid.Parse("E981D6BA-4CC3-4BF8-B1CC-5F78A4E0578D"),
                Name = "Matheus",
                Address = "Av. Natal",
                Balance = 1000000
            },
            new Customer{
                Id = Guid.Parse("427B9E92-A316-4AD6-853F-E488E3EE3972"),
                Name = "Agnaldo",
                Address = "Rua Francisco",
                Balance = 50
            }
        });


        dbContext.Products.AddRange(new List<Product>
        {
            new Product{
                Id = Guid.NewGuid(),
                Name = "PETR4",
                Description = "Petrobras",
                InitialPrice = 5,
                CurrentPrice = 5,
                ExpirationDate = DateTime.Now.AddYears(1),
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new Product{
                Id = Guid.NewGuid(),
                Name = "VALE3",
                Description = "Vale",
                InitialPrice = 10,
                CurrentPrice = 14,
                ExpirationDate = DateTime.Now.AddDays(30),
                IsActive = true,
                CreatedDate = DateTime.Now
            }
        });

        dbContext.SaveChanges();

    }
}
