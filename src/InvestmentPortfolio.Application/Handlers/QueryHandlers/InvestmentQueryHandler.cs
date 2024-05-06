using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Investments;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.QueryHandlers;
public class InvestmentQueryHandler : IRequestHandler<GetInvestmentQuery, IPagedList<InvestmentSummary>>,
                                      IRequestHandler<GetInvestmentByProductQuery, InvestmentDetails>
{
    private readonly ICustomerService _customerService;

    public InvestmentQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<IPagedList<InvestmentSummary>> Handle(GetInvestmentQuery request, CancellationToken cancellationToken)
    {
        return await _customerService.GetInvestmentByQuery(request.CustomerId, request.Page, request.PageSize);
    }

    public async Task<InvestmentDetails> Handle(GetInvestmentByProductQuery request, CancellationToken cancellationToken)
    {
        return await _customerService.GetInvestmentByProduct(request.CustomerId, request.ProductId);
    }
}
