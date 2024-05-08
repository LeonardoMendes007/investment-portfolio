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
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 4",
                Description = "ABC",
                IsActive = false,
                ExpirationDate = DateTime.UtcNow.AddDays(-5),
                CurrentPrice = 2,
                InitialPrice = 5
            }
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindAll())
            .Returns(activeProducts.AsQueryable());

        // Act
        var result = (await _productService.GetAllAsync(false)).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.All(result, p => Assert.True(p.IsActive));
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
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Active Product 4",
                Description = "ABC",
                IsActive = false,
                ExpirationDate = DateTime.UtcNow.AddDays(-5),
                CurrentPrice = 2,
                InitialPrice = 5
            }
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindAll())
            .Returns(activeProducts.AsQueryable());

        // Act
        var result = (await _productService.GetAllAsync(true)).ToList();

        // Assert
        Assert.NotNull(result);
        Assert.All(result, p => Assert.False(p.IsActive));
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

    [Fact]
    public async Task UpdateAsync_ValidProduct_UpdatesProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var existingProduct = new Product
        {
            Id = productId,
            Name = "Existing Product",
            Description = "Description of existing product",
            InitialPrice = 100,
            CurrentPrice = 120,
            ExpirationDate = DateTime.UtcNow.AddDays(30),
            IsActive = true
        };

        var updatedProduct = new Product
        {
            Id = productId,
            Name = "Updated Product",
            Description = "Updated description",
            InitialPrice = 150,
            CurrentPrice = 180,
            ExpirationDate = DateTime.UtcNow.AddDays(45),
            IsActive = false
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByNameAsync(updatedProduct.Name));

        // Act
        await _productService.UpdateAsync(updatedProduct);

        // Assert
        Assert.Equal(productId, updatedProduct.Id);
        Assert.Equal(updatedProduct.Name, existingProduct.Name);
        Assert.Equal(updatedProduct.Description, existingProduct.Description);
        Assert.Equal(updatedProduct.InitialPrice, existingProduct.InitialPrice);
        Assert.Equal(updatedProduct.CurrentPrice, existingProduct.CurrentPrice);
        Assert.Equal(updatedProduct.ExpirationDate, existingProduct.ExpirationDate);
        Assert.Equal(updatedProduct.IsActive, existingProduct.IsActive);
    }

    [Fact]
    public async Task UpdateAsync_InvalidId_ReturnsResourceNotFoundException()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var notExistingProduct = new Product
        {
            Id = productId
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId));

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            // Act
            await _productService.UpdateAsync(notExistingProduct);
        });
    }

    [Fact]
    public async Task UpdateAsync_AlreadyExistsName_ReturnsResourceAlreadyExistsException()
    {
        // Arrange
        var productId = Guid.NewGuid();

        var updatedProduct = new Product
        {
            Id = productId,
            Name = "Updated Product",
        };

        var existingProduct = new Product
        {
            Id = productId,
            Name = "Existing Product"
        };

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByIdAsync(productId))
            .ReturnsAsync(existingProduct);

        _mockUnitOfWork.Setup(x => x.ProductRepository.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(updatedProduct);

        // Assert
        await Assert.ThrowsAsync<ResourceAlreadyExistsException>(async () =>
        {
            // Act
            await _productService.UpdateAsync(updatedProduct);
        });
    }
}
