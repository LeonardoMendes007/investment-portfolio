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
        builder.Property(x => x.ContactInfo).HasColumnName("contact").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Balance).HasColumnName("balance").HasMaxLength(50).IsRequired();
        builder.HasKey(x => x.Id);

        builder.HasData(new List<Customer>
        { 
            new Customer{
            Id = Guid.NewGuid(),
            Name = "Leonardo",
            Address = "Rua tals",
            ContactInfo = "contato",
            Balance = 1000
            },
            new Customer{
            Id = Guid.NewGuid(),
            Name = "Ivana",
            Address = "Av. Paulista",
            ContactInfo = "contato",
            Balance = 1000000
            },
            new Customer{
            Id = Guid.NewGuid(),
            Name = "Guilherme",
            Address = "Rua Francisco",
            ContactInfo = "contato",
            Balance = 50
            }
        });

    }
}
