using InvestmentPortfolio.API.QueryParams;
using InvestmentPortfolio.API.Request;
using InvestmentPortfolio.API.Request.Product;
using InvestmentPortfolio.API.Response;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Product;
using InvestmentPortfolio.Application.Queries.Transaction;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
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
    [ProducesResponseType<ResponseBase<ProductDetails>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseBase>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var productDetails = await _mediator.Send(new GetProductByIdQuery()
        {
            Id = id
        });

        return Ok(ResponseBase<ProductDetails>.ResponseBaseFactory(productDetails, HttpStatusCode.OK));
    }

    [HttpGet]
    [ProducesResponseType<ResponseBase<IPagedList<ProductSummary>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetProductsQueryParams getProductsQueryParams)
    {

        var getMoviesQuery = new GetProductQuery()
        {
            Inactive = getProductsQueryParams.Inactive,
            Page = getProductsQueryParams.Page,
            PageSize = getProductsQueryParams.PageSize
        };

        var products = await _mediator.Send(getMoviesQuery);

        return Ok(ResponseBase<IPagedList<ProductSummary>>.ResponseBaseFactory(products, HttpStatusCode.OK));
    }

    [HttpGet("{id}/extract")]
    [ProducesResponseType<ResponseBase<IPagedList<ProductSummary>>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTransactionsAsync([FromRoute] Guid id,[FromQuery] PagedListQueryParams pagedListQueryParams)
    {

        var getTransactionByProduct = new GetTransactionByProductQuery()
        {
            ProductId = id,
            Page = pagedListQueryParams.Page,
            PageSize = pagedListQueryParams.PageSize
        };

        var transactions = await _mediator.Send(getTransactionByProduct);

        return Ok(ResponseBase<IPagedList<TransactionDetails>>.ResponseBaseFactory(transactions, HttpStatusCode.OK));
    }


    [HttpPost]
    [ProducesResponseType<ResponseBase>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
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

    [HttpPut("{id}")]
    [ProducesResponseType<ResponseBase>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateProductRequest productRequest)
    {

        var updateProductCommand = new UpdateProductCommand()
        {
            Id = id,
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
