using AutoMapper;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Responses.Details;
using InvestmentPortfolio.Domain.Entities.Investment;
using InvestmentPortfolio.Domain.Entities.Product;

namespace InvestmentPortfolio.Application.Mapper;
public class DomainToResponse : Profile
{
    public DomainToResponse()
    {
        #region Product to ProductDetails

        CreateMap<Product, ProductDetails>();

        #endregion

        #region Investment to InvestmentDetails

        CreateMap<Investment, InvestmentDetails>();

        #endregion
    }
}
