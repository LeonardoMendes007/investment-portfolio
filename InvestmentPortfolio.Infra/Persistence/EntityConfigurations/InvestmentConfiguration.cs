using InvestmentPortfolio.Domain.Entities.Investment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolio.Infra.Persistence.EntityConfigurations;
public class InvestmentConfiguration : IEntityTypeConfiguration<Investment>
{
    public void Configure(EntityTypeBuilder<Investment> builder)
    {
        builder.ToTable("tb_investment");
        builder.Property(x => x.CustomerId).HasColumnName("customerId").HasMaxLength(36).IsRequired();
        builder.Property(x => x.ProductId).HasColumnName("productId").HasMaxLength(36).IsRequired();
        builder.Property(x => x.Quantity).HasColumnName("quantity").IsRequired();
        builder.Property(x => x.InvestmentAmount).HasColumnName("investment_amount").HasPrecision(2).IsRequired();
        
        builder.HasKey(x => new {x.CustomerId, x.ProductId});

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Investments)
            .HasForeignKey(r => r.ProductId);

        builder.HasOne(i => i.Customer)
            .WithMany(c => c.Investments)
            .HasForeignKey(r => r.CustomerId);

    }
}
