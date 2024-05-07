﻿using AutoMapper;
using InvestmentPortfolio.Application.Pagination;
using InvestmentPortfolio.Application.Pagination.Interface;
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

    public async Task<IPagedList<ProductSummary>> GetAllAsync(int page, int pageSize)
    {
        var products = _unitOfWork.ProductRepository.FindAll();

        var productsSummary = products
            .Select(p => new ProductSummary
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CurrentPrice = p.CurrentPrice,
                ExpirationDate = p.ExpirationDate
            });

        var pagedListProducts = PagedList<ProductSummary>.CreatePagedList(productsSummary, page, pageSize);

        return pagedListProducts;
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

        if (await _unitOfWork.ProductRepository.FindByNameAsync(product.Name) is not null)
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
