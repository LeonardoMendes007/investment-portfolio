﻿// <auto-generated />
using System;
using InvestmentPortfolio.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvestmentPortfolio.Infra.Migrations
{
    [DbContext(typeof(InvestimentPortfolioDbContext))]
    [Migration("20240506160111_RemoveContactInfo")]
    partial class RemoveContactInfo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Customer.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("address");

                    b.Property<decimal>("Balance")
                        .HasMaxLength(50)
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("balance");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("tb_customer", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fe232d84-be96-4669-954c-215b65f6dbe4"),
                            Address = "Av. Itaquera",
                            Balance = 1000m,
                            Name = "Leonardo"
                        },
                        new
                        {
                            Id = new Guid("e981d6ba-4cc3-4bf8-b1cc-5f78a4e0578d"),
                            Address = "Av. Natal",
                            Balance = 1000000m,
                            Name = "Matheus"
                        },
                        new
                        {
                            Id = new Guid("427b9e92-a316-4ad6-853f-e488e3ee3972"),
                            Address = "Rua Francisco",
                            Balance = 50m,
                            Name = "Agnaldo"
                        });
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Investment.Investment", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customerId");

                    b.Property<Guid>("ProductId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productId");

                    b.Property<decimal>("InvestmentAmount")
                        .HasPrecision(10)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("investment_amount");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("tb_investment", (string)null);
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Product.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("dt_created");

                    b.Property<decimal>("CurrentPrice")
                        .HasPrecision(7)
                        .HasColumnType("decimal(7,2)")
                        .HasColumnName("current_price");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("InitialPrice")
                        .HasPrecision(7)
                        .HasColumnType("decimal(7,2)")
                        .HasColumnName("inital_price");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("dt_update");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tb_product", (string)null);
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Transaction.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id");

                    b.Property<Guid>("CustomerId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("customerId");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2")
                        .HasColumnName("dt_transaction");

                    b.Property<decimal>("PU")
                        .HasPrecision(7)
                        .HasColumnType("decimal(7,2)")
                        .HasColumnName("pu");

                    b.Property<Guid>("ProductId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("productId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("tb_transaction", (string)null);
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Investment.Investment", b =>
                {
                    b.HasOne("InvestmentPortfolio.Domain.Entities.Customer.Customer", "Customer")
                        .WithMany("Investments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentPortfolio.Domain.Entities.Product.Product", "Product")
                        .WithMany("Investments")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Transaction.Transaction", b =>
                {
                    b.HasOne("InvestmentPortfolio.Domain.Entities.Customer.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvestmentPortfolio.Domain.Entities.Product.Product", "Product")
                        .WithMany("Transactions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Customer.Customer", b =>
                {
                    b.Navigation("Investments");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("InvestmentPortfolio.Domain.Entities.Product.Product", b =>
                {
                    b.Navigation("Investments");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}