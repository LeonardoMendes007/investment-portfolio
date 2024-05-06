using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestmentPortfolio.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemovePrecisionInvestmentAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("4341efe5-6079-4971-999e-bc535bb106d3"));

            migrationBuilder.DeleteData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("79fb1594-d47d-4865-a483-dfc73fcde85a"));

            migrationBuilder.DeleteData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("84328e3a-22d9-450f-ac69-439c8277029b"));

            migrationBuilder.AlterColumn<decimal>(
                name: "investment_amount",
                table: "tb_investment",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(2,2)",
                oldPrecision: 2);

            migrationBuilder.InsertData(
                table: "tb_customer",
                columns: new[] { "id", "address", "balance", "contact", "name" },
                values: new object[,]
                {
                    { new Guid("427b9e92-a316-4ad6-853f-e488e3ee3972"), "Rua Francisco", 50m, "contato", "Guilherme" },
                    { new Guid("e981d6ba-4cc3-4bf8-b1cc-5f78a4e0578d"), "Rua tals", 1000m, "contato", "Leonardo" },
                    { new Guid("fe232d84-be96-4669-954c-215b65f6dbe4"), "Av. Paulista", 1000000m, "contato", "Ivana" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("427b9e92-a316-4ad6-853f-e488e3ee3972"));

            migrationBuilder.DeleteData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("e981d6ba-4cc3-4bf8-b1cc-5f78a4e0578d"));

            migrationBuilder.DeleteData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("fe232d84-be96-4669-954c-215b65f6dbe4"));

            migrationBuilder.AlterColumn<decimal>(
                name: "investment_amount",
                table: "tb_investment",
                type: "decimal(2,2)",
                precision: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "tb_customer",
                columns: new[] { "id", "address", "balance", "contact", "name" },
                values: new object[,]
                {
                    { new Guid("4341efe5-6079-4971-999e-bc535bb106d3"), "Rua tals", 1000m, "contato", "Leonardo" },
                    { new Guid("79fb1594-d47d-4865-a483-dfc73fcde85a"), "Rua Francisco", 50m, "contato", "Guilherme" },
                    { new Guid("84328e3a-22d9-450f-ac69-439c8277029b"), "Av. Paulista", 1000000m, "contato", "Ivana" }
                });
        }
    }
}
