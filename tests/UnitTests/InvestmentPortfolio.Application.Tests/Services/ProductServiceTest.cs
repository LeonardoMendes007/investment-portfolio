using InvestmentPortfolio.Application.Services;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Exceptions;
using InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
using Moq;

namespace InvestmentPortfolio.Application.Tests.Services;
public class ProductServiceTest
{
    private readonly MockRepository _mockRepository = new MockRepository(MockBehavior.Loose);

    private readonly Mock<IUnitOfWork> _mockUnitOfWork;

    private readonly IProductService _productService;

    public ProductServiceTest()
    {
        _mockUnitOfWork = _mockRepository.Create<IUnitOfWork>();

        _productService = new ProductService(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Name = "Existing Product",
            Description = "ABC",
            IsActive = true,
            ExpirationDate = DateTime.UtcNow.AddDays(30),
            CurrentPrice = 100,
            InitialPrice = 50,
            CreatedDate = DateTime.UtcNow.AddDays(-10),
            UpdatedDate = DateTime.UtcNow.AddDays(-5)
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId))
            .ReturnsAsync(product);

        // Act
        var result = await _productService.GetByIdAsync(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(productId, result.Id);
        Assert.Equal(product.Name, result.Name);
        Assert.Equal(product.Description, result.Description);
        Assert.Equal(product.ExpirationDate, result.ExpirationDate);
        Assert.Equal(product.IsActive, result.IsActive);
        Assert.Equal(product.CreatedDate, result.CreatedDate);
        Assert.Equal(product.UpdatedDate, result.UpdatedDate);
    }

    [Fact]
    public async Task GetByIdAsync_InvalidId_ReturnsResourceNotFoundException()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _productService.GetByIdAsync(productId);
        });
    }

    [Fact]
    public async Task GetAllAsync_ActiveProducts_ReturnsActiveProducts()
    {
        // Arrange
        var activeProducts = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 1",
                Description = "CBA",
                IsActive = true,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                CurrentPrice = 100,
                InitialPrice = 50,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 2",
                Description = "ABC",
                IsActive = true,
                ExpirationDate = DateTime.UtcNow.AddDays(15),
                CurrentPrice = 200,
                InitialPrice = 300
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 3",
                Description = "ABC",
                IsActive = false,
                ExpirationDate = DateTime.UtcNow.AddDays(5),
                CurrentPrice = 5,
                InitialPrice = 10
            }
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindAll())
            .Returns(activeProducts.AsQueryable());

        // Act
        var result = (await _productService.GetAllAsync(false)).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.All(result, p => Assert.True(p.IsActive));
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(activeProducts[i].Id, result[i].Id);
            Assert.Equal(activeProducts[i].Name, result[i].Name);
            Assert.Equal(activeProducts[i].Description, result[i].Description);
            Assert.Equal(activeProducts[i].ExpirationDate, result[i].ExpirationDate);
            Assert.Equal(activeProducts[i].CurrentPrice, result[i].CurrentPrice);
            Assert.Equal(activeProducts[i].InitialPrice, result[i].InitialPrice);
            Assert.Equal(activeProducts[i].IsActive, result[i].IsActive);
        }
    }

    [Fact]
    public async Task GetAllAsync_InactiveProducts_ReturnsActiveProducts()
    {
        // Arrange
        var activeProducts = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 1",
                Description = "CBA",
                IsActive = true,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                CurrentPrice = 100,
                InitialPrice = 50,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 2",
                Description = "ABC",
                IsActive = true,
                ExpirationDate = DateTime.UtcNow.AddDays(15),
                CurrentPrice = 200,
                InitialPrice = 300
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 3",
                Description = "ABC",
                IsActive = false,
                ExpirationDate = DateTime.UtcNow.AddDays(5),
                CurrentPrice = 5,
                InitialPrice = 10
            }
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindAll())
            .Returns(activeProducts.AsQueryable());

        // Act
        var result = (await _productService.GetAllAsync(false)).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.All(result, p => Assert.True(p.IsActive));
        for (int i = 0; i < result.Count; i++)
        {
            Assert.Equal(activeProducts[i].Id, result[i].Id);
            Assert.Equal(activeProducts[i].Name, result[i].Name);
            Assert.Equal(activeProducts[i].Description, result[i].Description);
            Assert.Equal(activeProducts[i].ExpirationDate, result[i].ExpirationDate);
            Assert.Equal(activeProducts[i].CurrentPrice, result[i].CurrentPrice);
            Assert.Equal(activeProducts[i].InitialPrice, result[i].InitialPrice);
            Assert.Equal(activeProducts[i].IsActive, result[i].IsActive);
        }
    }

    [Fact]
    public async Task CreateAsync_NewProduct_ReturnsProductId()
    {
        // Arrange
        var productName = "New Product";
        var product = new Product
        {
            Name = productName,
            IsActive = true,
            ExpirationDate = DateTime.UtcNow.AddDays(30),
            CurrentPrice = 100
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByNameAsync(productName));

        _mockUnitOfWork.Setup(x => x.ProductRepository.SaveAsync(It.IsAny<Product>()));
        _mockUnitOfWork.Setup(x => x.CommitAsync());

        // Act
        var result = await _productService.CreateAsync(product);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result, result);
    }

    [Fact]
    public async Task CreateAsync_NewProduct_ReturnsResourceAlreadyExistsException()
    {
        // Arrange
        var productName = "New Product";
        var product = new Product
        {
            Name = productName,
            IsActive = true,
            ExpirationDate = DateTime.UtcNow.AddDays(30),
            CurrentPrice = 100
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByNameAsync(productName))
            .ReturnsAsync(new Product());

        // Assert
        await Assert.ThrowsAsync<ResourceAlreadyExistsException>(async () =>
        {
            // Act
            var result = await _productService.CreateAsync(product);
        });
    }
}
