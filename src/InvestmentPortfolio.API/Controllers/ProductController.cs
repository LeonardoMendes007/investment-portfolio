﻿using InvestmentPortfolio.API.QueryParams;
using InvestmentPortfolio.API.Request;
using InvestmentPortfolio.API.Request.Product;
using InvestmentPortfolio.API.Response;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Product;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Domain.Entities.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentPortfolio.API.Controllers;
[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var productDetails = await _mediator.Send(new GetProductByIdQuery()
        {
            Id = id
        });

        return Ok(ResponseBase<ProductDetails>.ResponseBaseFactory(productDetails, HttpStatusCode.OK));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PagedListQueryParams pagedListQueryParams)
    {

        var getMoviesQuery = new GetProductQuery()
        {
            Page = pagedListQueryParams.Page,
            PageSize = pagedListQueryParams.PageSize
        };

        var products = await _mediator.Send(getMoviesQuery);

        return Ok(ResponseBase<IPagedList<ProductSummary>>.ResponseBaseFactory(products, HttpStatusCode.OK));
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequest productRequest)
    {

        var createProductCommand = new CreateProductCommand()
        {
            Name = productRequest.Name,
            Price = productRequest.Price,
            Description = productRequest.Description,
            ExpirationDate = productRequest.ExpirationDate,
            IsActive = productRequest.IsActive
        };

        var productId = await _mediator.Send(createProductCommand);

        return CreatedAtRoute("GetById", new { id = productId }, ResponseBase.ResponseBaseFactory(HttpStatusCode.Created));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateProductRequest productRequest)
    {

        var updateProductCommand = new UpdateProductCommand()
        {
            Name = productRequest.Name,
            InitialPrice = productRequest.InitialPrice,
            CurrentPrice = productRequest.CurrentPrice,
            Description = productRequest.Description,
            ExpirationDate = productRequest.ExpirationDate,
            IsActive = productRequest.IsActive
        };
        await _mediator.Send(updateProductCommand);

        return Ok(ResponseBase.ResponseBaseFactory(HttpStatusCode.OK));
    }

}