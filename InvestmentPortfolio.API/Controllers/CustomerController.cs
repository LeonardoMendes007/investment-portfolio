using InvestmentPortfolio.API.QueryParams;
using InvestmentPortfolio.API.Response;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Investments;
using InvestmentPortfolio.Application.Queries.Transactions;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentPortfolio.API.Controllers;
[Route("api/customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}/investments")]
    public async Task<IActionResult> GetInvesmentsAsync([FromRoute] Guid id, [FromQuery] PagedListQueryParams pagedListQueryParams)
    {

        var getInvestmentQuery = new GetInvestmentQuery()
        {
            CustomerId = id,
            Page = pagedListQueryParams.Page,
            PageSize = pagedListQueryParams.PageSize
            
        };

        var investments = await _mediator.Send(getInvestmentQuery);

        return Ok(ResponseBase<IPagedList<InvestmentSummary>>.ResponseBaseFactory(investments, HttpStatusCode.OK));
    }

    [HttpGet("{id}/investments/{productId}")]
    public async Task<IActionResult> GetInvesmentsByProductIdAsync([FromRoute] Guid id, [FromRoute] Guid productId)
    {

        var getInvestmentByProductQuery = new GetInvestmentByProductQuery()
        {
            CustomerId = id,
            ProductId = productId
        };

        var investments = await _mediator.Send(getInvestmentByProductQuery);

        return Ok(ResponseBase<InvestmentDetails>.ResponseBaseFactory(investments, HttpStatusCode.OK));
    }

    [HttpGet("{id}/transactions")]
    public async Task<IActionResult> GetTransactionsAsync([FromRoute] Guid id, [FromQuery] PagedListQueryParams pagedListQueryParams)
    {
        var getTransactionQuery = new GetTransactionQuery()
        {
            CustomerId = id,
            Page = pagedListQueryParams.Page,
            PageSize = pagedListQueryParams.PageSize

        };

        var transactions = await _mediator.Send(getTransactionQuery);

        return Ok(ResponseBase<IPagedList<TransactionSummary>>.ResponseBaseFactory(transactions, HttpStatusCode.OK));
    }

    [HttpGet("{id}/transactions/{productId}")]
    public async Task<IActionResult> GetTransactionsByProductIdAsync([FromRoute] Guid id, [FromRoute] Guid productId, [FromQuery] PagedListQueryParams pagedListQueryParams)
    {

        var getTransactionByProductQuery = new GetTransactionByProductQuery()
        {
            CustomerId = id,
            ProductId = productId,
            Page = pagedListQueryParams.Page,
            PageSize = pagedListQueryParams.PageSize
        };

        var transactions = await _mediator.Send(getTransactionByProductQuery);

        return Ok(ResponseBase<IPagedList<TransactionDetails>>.ResponseBaseFactory(transactions, HttpStatusCode.OK));
    }
}
