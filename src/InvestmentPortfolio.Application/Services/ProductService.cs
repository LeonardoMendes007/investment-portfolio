using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.FindByIdAsync(id);

        if (product is null)
        {
            throw new ResourceNotFoundException(id);
        }

        return product;
    }

    public async Task<IQueryable<Product>> GetAllAsync(bool inactive)
    {
        var products = _unitOfWork.ProductRepository.FindAll();

        products = products.Where(p => (p.ExpirationDate >= DateTime.Now ? p.IsActive : false) == !inactive);

        return products;
    }

    public async Task<Guid> CreateAsync(Product product)
    {
        if (await _unitOfWork.ProductRepository.FindByNameAsync(product.Name) is not null)
        {
            throw new ResourceAlreadyExistsException($"Product already exists with Name = {product.Name}.");
        }

        product.Id = Guid.NewGuid();
        await _unitOfWork.ProductRepository.SaveAsync(product);
        await _unitOfWork.CommitAsync();

        return product.Id;  
    }

    public async Task UpdateAsync(Product product)
    {
        var productPersistence = await _unitOfWork.ProductRepository.FindByIdAsync(product.Id);

        if (productPersistence is null)
        {
            throw new ResourceNotFoundException(product.Id);
        }

        if (!productPersistence.Name.Equals(product.Name) && (await _unitOfWork.ProductRepository.FindByNameAsync(product.Name)) is not null)
        {
            throw new ResourceAlreadyExistsException($"Product already exists with Name = {product.Name}.");
        }

        productPersistence.Name = product.Name;
        productPersistence.Description = product.Description;
        productPersistence.InitialPrice = product.InitialPrice;
        productPersistence.CurrentPrice = product.CurrentPrice;
        productPersistence.ExpirationDate = product.ExpirationDate;
        productPersistence.IsActive = product.IsActive;

        productPersistence.UpdatedDate = DateTime.Now;

        await _unitOfWork.CommitAsync();
    }

    public async Task<IQueryable<Transaction>> GetAllTransactionsAsync(Guid id)
    {
        var transactions = _unitOfWork.ProductRepository.FindAllTransations(id);

        transactions = transactions.OrderByDescending(x => x.Date);

        return transactions;
    }

    
}
