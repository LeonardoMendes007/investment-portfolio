using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Transactions;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.QueryHandlers;
public class TransactionQueryHandler : IRequestHandler<GetTransactionQuery, IPagedList<TransactionSummary>>,
                                       IRequestHandler<GetTransactionByProductQuery, IPagedList<TransactionDetails>>
{
    private readonly ICustomerService _customerService;

    public TransactionQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public Task<IPagedList<TransactionSummary>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        return _customerService.GetTransactionByQuery(request.CustomerId, request.Page, request.PageSize);
    }

    public Task<IPagedList<TransactionDetails>> Handle(GetTransactionByProductQuery request, CancellationToken cancellationToken)
    {
        return _customerService.GetTransactionsByProduct(request.CustomerId, request.ProductId, request.Page, request.PageSize);
    }
}
