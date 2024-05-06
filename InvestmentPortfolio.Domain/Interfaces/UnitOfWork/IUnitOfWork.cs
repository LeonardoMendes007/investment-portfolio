using InvestmentPortfolio.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPortfolio.Domain.Interfaces.UnitOfWork;
public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ITransactionRepository TransactionRepository { get; }
    IInvestmentRepository InvestmentRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    Task CommitAsync();
}