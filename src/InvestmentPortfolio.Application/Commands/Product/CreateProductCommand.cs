﻿using MediatR;

namespace InvestmentPortfolio.Application.Commands.Product;
public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
}
