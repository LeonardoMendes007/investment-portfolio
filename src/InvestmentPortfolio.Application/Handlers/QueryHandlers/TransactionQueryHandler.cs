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
        if(await _productService.GetByIdAsync(request.ProductId) is null)
        {
            throw new ResourceNotFoundException(request.ProductId);
        }

        return await _productService.GetAllTransactionsAsync(request.ProductId, request.Page, request.PageSize);
    }

    public async Task<IPagedList<TransactionSummary>> Handle(GetTransactionByCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _customerService.GetTransactionByQueryAsync(request.CustomerId, request.Page, request.PageSize);
    }

    public Task<IPagedList<TransactionDetails>> Handle(GetTransactionByCustomerAndProductQuery request, CancellationToken cancellationToken)
    {
        return _customerService.GetTransactionsByProductAsync(request.CustomerId, request.ProductId, request.Page, request.PageSize);
    }

   
}
