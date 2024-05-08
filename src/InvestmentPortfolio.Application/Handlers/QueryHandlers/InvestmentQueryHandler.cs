using AutoMapper;
using InvestmentPortfolio.Application.Pagination;
using InvestmentPortfolio.Application.Pagination.Interface;
using InvestmentPortfolio.Application.Queries.Investments;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Application.Responses.Summary;
using InvestmentPortfolio.Application.Services.Interfaces;
using InvestmentPortfolio.Domain.Entities.Investment;
using MediatR;

namespace InvestmentPortfolio.Application.Handlers.QueryHandlers;
public class InvestmentQueryHandler : IRequestHandler<GetInvestmentQuery, IPagedList<InvestmentSummary>>,
                                      IRequestHandler<GetInvestmentByProductQuery, InvestmentDetails>
{
    private readonly ICustomerService _customerService;
    private readonly IMapper _mapper;

    public InvestmentQueryHandler(ICustomerService customerService, IMapper mapper)
    {
        _customerService = customerService;
        _mapper = mapper;
    }

    public async Task<IPagedList<InvestmentSummary>> Handle(GetInvestmentQuery request, CancellationToken cancellationToken)
    {
        var investments =  await _customerService.GetInvestmentByQueryAsync(request.CustomerId);

        var investmentsSummary = investments
                .Select(t => new InvestmentSummary
                {
                    ProductId = t.ProductId,
                    ProductName = t.Product.Name,
                    Quantity = t.Quantity,
                    InvestmentAmount = t.InvestmentAmount,
                    CurrentAmount = t.CurrentAmount
                });

        return PagedList<InvestmentSummary>.CreatePagedList(investmentsSummary, request.Page, request.PageSize);
    }

    public async Task<InvestmentDetails> Handle(GetInvestmentByProductQuery request, CancellationToken cancellationToken)
    {
        var investment = await _customerService.GetInvestmentByProductAsync(request.CustomerId, request.ProductId);

        return _mapper.Map<InvestmentDetails>(investment);
    }
}
