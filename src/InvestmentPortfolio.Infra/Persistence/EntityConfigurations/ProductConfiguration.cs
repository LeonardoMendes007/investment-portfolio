using InvestmentPortfolio.Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolio.Infra.Persistence.EntityConfigurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("tb_product");
        builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(x => x.Description).HasColumnName("description").HasMaxLength(300).IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.Property(x => x.InitialPrice).HasColumnName("inital_price").HasPrecision(7,2).IsRequired();
        builder.Property(x => x.CurrentPrice).HasColumnName("current_price").HasPrecision(7, 2).IsRequired();
        builder.Property(x => x.IsActive).HasColumnName("isActive").IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnName("dt_created").IsRequired();
        builder.Property(x => x.UpdatedDate).HasColumnName("dt_update");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name).IsUnique();

    }
}
