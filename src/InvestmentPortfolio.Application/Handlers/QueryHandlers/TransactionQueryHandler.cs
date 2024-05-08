using InvestmentPortfolio.Application.Pagination;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Transaction;
using InvestmentPortfolio.Application.Queries.Transactions;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Exceptions;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.QueryHandlers;
public class TransactionQueryHandler : IRequestHandler<GetTransactionByProductQuery, IPagedList<TransactionDetails>>,
                                       IRequestHandler<GetTransactionByCustomerQuery, IPagedList<TransactionSummary>>,
                                       IRequestHandler<GetTransactionByCustomerAndProductQuery, IPagedList<TransactionDetails>>
{
    private readonly ICustomerService _customerService;
    private readonly IProductService _productService;

    public TransactionQueryHandler(ICustomerService customerService, IProductService productService)
    {
        _customerService = customerService;
        _productService = productService;
    }

    public async Task<IPagedList<TransactionDetails>> Handle(GetTransactionByProductQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _productService.GetAllTransactionsAsync(request.ProductId);

        var transactionsDetails = transactions
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

        return PagedList<TransactionDetails>.CreatePagedList(transactionsDetails, request.Page, request.PageSize);
    }

    public async Task<IPagedList<TransactionSummary>> Handle(GetTransactionByCustomerQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _customerService.GetTransactionByQueryAsync(request.CustomerId);

        var transactionsSummary = transactions
           .Select(t => new TransactionSummary
           {
               Id = t.Id,
               ProductId = t.ProductId,
               CustomerId = t.CustomerId,
               ProductName = t.Product.Name,
               TransactionType = t.TransactionType,
               TransactionTypeName = t.TransactionType == 0 ? "Buy" : "Sell",
               Amount = t.PU * t.Quantity,
               Quantity = t.Quantity,
               Date = t.Date
           });

        return PagedList<TransactionSummary>.CreatePagedList(transactionsSummary, request.Page, request.PageSize);

    }

    public async Task<IPagedList<TransactionDetails>> Handle(GetTransactionByCustomerAndProductQuery request, CancellationToken cancellationToken)
    {
        var transactions = await _customerService.GetTransactionsByProductAsync(request.CustomerId, request.ProductId);

        var transactionsDetails = transactions
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

        return PagedList<TransactionDetails>.CreatePagedList(transactionsDetails, request.Page, request.PageSize);
    }
}
