using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentPortfolio.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemoveContactInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "contact",
                table: "tb_customer");

            migrationBuilder.AlterColumn<decimal>(
                name: "pu",
                table: "tb_transaction",
                type: "decimal(7,2)",
                precision: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "inital_price",
                table: "tb_product",
                type: "decimal(7,2)",
                precision: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "current_price",
                table: "tb_product",
                type: "decimal(7,2)",
                precision: 7,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "investment_amount",
                table: "tb_investment",
                type: "decimal(10,2)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "balance",
                table: "tb_customer",
                type: "decimal(10,2)",
                maxLength: 50,
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("427b9e92-a316-4ad6-853f-e488e3ee3972"),
                column: "name",
                value: "Agnaldo");

            migrationBuilder.UpdateData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("e981d6ba-4cc3-4bf8-b1cc-5f78a4e0578d"),
                columns: new[] { "address", "balance", "name" },
                values: new object[] { "Av. Natal", 1000000m, "Matheus" });

            migrationBuilder.UpdateData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("fe232d84-be96-4669-954c-215b65f6dbe4"),
                columns: new[] { "address", "balance", "name" },
                values: new object[] { "Av. Itaquera", 1000m, "Leonardo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "pu",
                table: "tb_transaction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "inital_price",
                table: "tb_product",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "current_price",
                table: "tb_product",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,2)",
                oldPrecision: 7);

            migrationBuilder.AlterColumn<decimal>(
                name: "investment_amount",
                table: "tb_investment",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "balance",
                table: "tb_customer",
                type: "decimal(18,2)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldMaxLength: 50,
                oldPrecision: 10);

            migrationBuilder.AddColumn<string>(
                name: "contact",
                table: "tb_customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("427b9e92-a316-4ad6-853f-e488e3ee3972"),
                columns: new[] { "contact", "name" },
                values: new object[] { "contato", "Guilherme" });

            migrationBuilder.UpdateData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("e981d6ba-4cc3-4bf8-b1cc-5f78a4e0578d"),
                columns: new[] { "address", "balance", "contact", "name" },
                values: new object[] { "Rua tals", 1000m, "contato", "Leonardo" });

            migrationBuilder.UpdateData(
                table: "tb_customer",
                keyColumn: "id",
                keyValue: new Guid("fe232d84-be96-4669-954c-215b65f6dbe4"),
                columns: new[] { "address", "balance", "contact", "name" },
                values: new object[] { "Av. Paulista", 1000000m, "contato", "Ivana" });
        }
    }
}
