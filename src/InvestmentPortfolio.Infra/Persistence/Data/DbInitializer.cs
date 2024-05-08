using InvestmentPortfolio.Domain.Entities.Customer;
using InvestmentPortfolio.Domain.Entities.Product;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace InvestmentPortfolio.Infra.Persistence.Data;
public static class DbInitializer 
{
    public static void Seed(IServiceProvider provider)
    {
        var dbContext = provider.GetService<InvestimentPortfolioDbContext>();

        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = Path.Combine(basePath, "Persistence", "Data","Mock");

        var jsonCustomers = File.ReadAllText(Path.Combine(filePath, "customers.json"));
        var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonCustomers);

        var jsonProducts = File.ReadAllText(Path.Combine(filePath, "products.json"));
        var products = JsonConvert.DeserializeObject<List<Product>>(jsonProducts);

        dbContext.Customers.AddRange(customers);
        
        dbContext.Products.AddRange(products);

        dbContext.SaveChanges();

    }
}
