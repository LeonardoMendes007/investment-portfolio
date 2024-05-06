using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPortfolio.Domain.Exceptions;
public class ProductIsInativeException : Exception
{
    public Guid Id { get; set; }

    public ProductIsInativeException(Guid id) : base($"Product with id = {id}, is inative.")
    {
        Id = id;
    }
}
