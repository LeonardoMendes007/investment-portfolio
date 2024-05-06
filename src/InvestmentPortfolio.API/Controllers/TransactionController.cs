using InvestmentPortfolio.API.Request.Transaction;
using InvestmentPortfolio.API.Response;
using InvestmentPortfolio.Application.Commands.Transaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvestmentPortfolio.API.Controllers;
[Route("api/transaction")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/buy")]
    public async Task<IActionResult> BuyAsync([FromBody] TransactionRequest transactionRequest)
    {
        var transactionBuyCommand = new TransactionBuyCommand()
        {
            CustomerId = transactionRequest.CustomerId,
            ProductId = transactionRequest.ProductId,
            Quantity = transactionRequest.Quantity
        };

        var transactionId = await _mediator.Send(transactionBuyCommand);

        return Ok(ResponseBase<Guid>.ResponseBaseFactory(transactionId, HttpStatusCode.OK));
    }

    [HttpPost("/sell")]
    public async Task<IActionResult> SellAsync([FromBody] TransactionRequest transactionRequest)
    {

        var transactionSellCommand = new TransactionSellCommand()
        {
            CustomerId = transactionRequest.CustomerId,
            ProductId = transactionRequest.ProductId,
            Quantity = transactionRequest.Quantity
        };

        var transactionId = await _mediator.Send(transactionSellCommand);

        return Ok(ResponseBase<Guid>.ResponseBaseFactory(transactionId, HttpStatusCode.OK));
    }

}
