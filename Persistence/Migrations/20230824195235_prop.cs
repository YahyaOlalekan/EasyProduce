using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionProduceTypes");

            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "Transactions",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "QuantityToBuy",
                table: "ProduceTypes",
                newName: "Quantity");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Transactions",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Transactions",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ProduceTypeId",
                table: "Transactions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "Transactions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ManagerId",
                table: "Transactions",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Managers_ManagerId",
                table: "Transactions",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Managers_ManagerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ManagerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProduceTypeId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Transactions",
                newName: "TotalQuantity");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProduceTypes",
                newName: "QuantityToBuy");

            migrationBuilder.CreateTable(
                name: "TransactionProduceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProduceTypeId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    TransactionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionProduceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionProduceTypes_ProduceTypes_ProduceTypeId",
                        column: x => x.ProduceTypeId,
                        principalTable: "ProduceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionProduceTypes_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProduceTypes_ProduceTypeId",
                table: "TransactionProduceTypes",
                column: "ProduceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProduceTypes_TransactionId",
                table: "TransactionProduceTypes",
                column: "TransactionId");
        }
    }
}
