using AutoMapper;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;

namespace InvestmentPortfolio.Application.Services;
public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    public async Task<IQueryable<ProductSummary>> GetAllAsync(bool inactive)
    {
        var products = _unitOfWork.ProductRepository.FindAll();

        var productsSummary = products
            .Select(p => new ProductSummary
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CurrentPrice = p.CurrentPrice,
                ExpirationDate = p.ExpirationDate,
                IsActive = p.ExpirationDate >= DateTime.Now ? p.IsActive : false
            });

        productsSummary = productsSummary.Where(p => p.IsActive == !inactive);

        return productsSummary;
    }

    public async Task<IQueryable<TransactionDetails>> GetAllTransactionsAsync(Guid id)
    {
        var transactionsQuery = _unitOfWork.ProductRepository.FindAllTransations(id);

        transactionsQuery = transactionsQuery.OrderByDescending(x => x.Date);

        var transactionsDetails = transactionsQuery
            .Select(t => new TransactionDetails
            {
                Id = t.Id,
                ProductId = t.ProductId,
                CustomerId = t.CustomerId,
                ProductName = t.Product.Name,
                PU = t.PU,
                Quantity = t.Quantity,
                TransactionType = t.TransactionType,
                TransactionTypeName = t.TransactionType == 0 ? "Buy" : "Sell",

                Date = t.Date
            });

        return transactionsDetails;
    }

    public async Task<ProductDetails> GetByIdAsync(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.FindByIdAsync(id);

        if(product is null)
        {
            throw new ResourceNotFoundException(id);
        }

        return _mapper.Map<ProductDetails>(product);
    }

    public async Task UpdateAsync(Product product)
    {
        var productPersistence = await _unitOfWork.ProductRepository.FindByIdAsync(product.Id);
        
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
}
