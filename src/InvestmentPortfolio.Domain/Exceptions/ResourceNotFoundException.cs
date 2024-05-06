using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPortfolio.Domain.Exceptions;
public class ResourceNotFoundException : Exception
{
    public Guid Id { get; set; }

    public ResourceNotFoundException(Guid id) : base($"Resourse not found with id = {id}.")
    {
        Id = id;
    }

    public ResourceNotFoundException(Guid id, string message) : base(message)
    {
        Id = id;
    }
}
