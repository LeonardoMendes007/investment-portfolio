using System.ComponentModel.DataAnnotations;

namespace InvestmentPortfolio.API.Request;

public class CreateProductRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(300)]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public DateTime ExpirationDate { get; set; }
    [Required] 
    public bool IsActive { get; set; }
}
