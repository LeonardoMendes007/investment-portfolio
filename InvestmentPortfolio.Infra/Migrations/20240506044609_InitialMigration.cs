using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestmentPortfolio.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_customer",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    contact = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_product",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    inital_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    current_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dt_update = table.Column<DateTime>(type: "datetime2", nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_investment",
                columns: table => new
                {
                    customerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    productId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    investment_amount = table.Column<decimal>(type: "decimal(2,2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_investment", x => new { x.customerId, x.productId });
                    table.ForeignKey(
                        name: "FK_tb_investment_tb_customer_customerId",
                        column: x => x.customerId,
                        principalTable: "tb_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_investment_tb_product_productId",
                        column: x => x.productId,
                        principalTable: "tb_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    customerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    productId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    pu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dt_transaction = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_transaction_tb_customer_customerId",
                        column: x => x.customerId,
                        principalTable: "tb_customer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_transaction_tb_product_productId",
                        column: x => x.productId,
                        principalTable: "tb_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tb_customer",
                columns: new[] { "id", "address", "balance", "contact", "name" },
                values: new object[,]
                {
                    { new Guid("4341efe5-6079-4971-999e-bc535bb106d3"), "Rua tals", 1000m, "contato", "Leonardo" },
                    { new Guid("79fb1594-d47d-4865-a483-dfc73fcde85a"), "Rua Francisco", 50m, "contato", "Guilherme" },
                    { new Guid("84328e3a-22d9-450f-ac69-439c8277029b"), "Av. Paulista", 1000000m, "contato", "Ivana" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_investment_productId",
                table: "tb_investment",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_product_name",
                table: "tb_product",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_transaction_customerId",
                table: "tb_transaction",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_transaction_productId",
                table: "tb_transaction",
                column: "productId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_investment");

            migrationBuilder.DropTable(
                name: "tb_transaction");

            migrationBuilder.DropTable(
                name: "tb_customer");

            migrationBuilder.DropTable(
                name: "tb_product");
        }
    }
}
