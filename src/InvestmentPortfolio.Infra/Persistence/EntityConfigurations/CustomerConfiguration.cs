using Humanizer.Localisation;
using InvestmentPortfolio.Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvestmentPortfolio.Infra.Persistence.EntityConfigurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("tb_customer");
        builder.Property(x => x.Id).HasColumnName("id").HasMaxLength(36);
        builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Address).HasColumnName("address").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Balance).HasColumnName("balance").HasMaxLength(50).HasPrecision(10).IsRequired();
        builder.HasKey(x => x.Id);

        builder.HasData(new List<Customer>
        {
            new Customer{
            Id = Guid.Parse("FE232D84-BE96-4669-954C-215B65F6DBE4"),
            Name = "Leonardo",
            Address = "Av. Itaquera",
            Balance = 1000
            },
            new Customer{
            Id = Guid.Parse("E981D6BA-4CC3-4BF8-B1CC-5F78A4E0578D"),
            Name = "Matheus",
            Address = "Av. Natal",
            Balance = 1000000
            },
            new Customer{
            Id = Guid.Parse("427B9E92-A316-4AD6-853F-E488E3EE3972"),
            Name = "Agnaldo",
            Address = "Rua Francisco",
            Balance = 50
            }
        });

    }
}
