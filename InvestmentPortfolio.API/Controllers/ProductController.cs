using InvestmentPortfolio.API.QueryParams;
using InvestmentPortfolio.API.Request;
using InvestmentPortfolio.API.Response;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Product;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _mediator.Send(new GetProductByIdQuery()
        {
            Id = id
        });

        return Ok(ResponseBase<ProductDetails>.ResponseBaseFactory(movie, HttpStatusCode.OK));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PagedListQueryParams pagedListQueryParams)
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
    public async Task<IActionResult> Create([FromBody] CreateProductRequest productRequest)
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

        return CreatedAtAction(nameof(Get), new { productId }, ResponseBase.ResponseBaseFactory(HttpStatusCode.Created));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest productRequest)
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

        return NoContent();
    }

}
