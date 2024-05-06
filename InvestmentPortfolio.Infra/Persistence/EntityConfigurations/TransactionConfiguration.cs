using InvestmentPortfolio.Domain.Entities.Transaction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolio.Infra.Persistence.EntityConfigurations;
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("tb_transaction");
        builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(x => x.CustomerId).HasColumnName("customerId").HasMaxLength(36).IsRequired();
        builder.Property(x => x.ProductId).HasColumnName("productId").HasMaxLength(36).IsRequired();
        builder.Property(x => x.Quantity).HasColumnName("quantity").IsRequired();
        builder.Property(x => x.PU).HasColumnName("pu").IsRequired();
        builder.Property(x => x.TransactionType).HasColumnName("type").IsRequired();
        builder.Property(x => x.Date).HasColumnName("dt_transaction").IsRequired();

        builder.HasKey(x => x.Id);

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Transactions)
            .HasForeignKey(r => r.ProductId);

        builder.HasOne(i => i.Customer)
            .WithMany(c => c.Transactions)
            .HasForeignKey(r => r.CustomerId);
    }
}
