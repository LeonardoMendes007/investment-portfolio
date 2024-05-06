using AutoMapper;
using InvestmentPortfolio.Application.Commands.Product;
using InvestmentPortfolio.Application.Commands.Transaction;
using InvestmentPortfolio.Domain.Entities.Product;
using InvestmentPortfolio.Domain.Entities.Transaction;

namespace InvestmentPortfolio.Application.Mapper;
public class CommandToDomain : Profile
{
    public CommandToDomain()
    {
        #region CreateProductCommand to Product

        CreateMap<CreateProductCommand, Product>()
            .ForMember(p => p.CurrentPrice, m => m.MapFrom(cpm => cpm.Price))
            .ForMember(p => p.InitialPrice, m => m.MapFrom(cpm => cpm.Price));

        #endregion

        #region UpdateProductCommand to Product

        CreateMap<UpdateProductCommand, Product>();

        #endregion

        #region TransactionBuyCommand to Transaction

        CreateMap<TransactionBuyCommand, Transaction>();

        #endregion

        #region TransactionSellCommand to Transaction

        CreateMap<TransactionSellCommand, Transaction>();

        #endregion

    }
}
