using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Migrations_MySqlServer.Migrations
{
    public partial class ChangesrelationshipbetweenCardandCardBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MerchantCNPJ",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "MerchantName",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Card");

            migrationBuilder.AddColumn<Guid>(
                name: "Login_Id",
                table: "Merchant",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Login_Id",
                table: "Customer",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CardBrandId",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardBrandId1",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CardBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBrand", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "CardBrand",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Visa" },
                    { 2, "Mastercard" },
                    { 3, "Amex" },
                    { 4, "Discover" },
                    { 5, "DinersClub" },
                    { 6, "JCB" },
                    { 99, "Invalid" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardBrandId1",
                table: "Card",
                column: "CardBrandId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_CardBrand_CardBrandId1",
                table: "Card",
                column: "CardBrandId1",
                principalTable: "CardBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Merchant_Id",
                table: "Transaction",
                column: "Id",
                principalTable: "Merchant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_CardBrand_CardBrandId1",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Merchant_Id",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "CardBrand");

            migrationBuilder.DropIndex(
                name: "IX_Card_CardBrandId1",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Login_Id",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "Login_Id",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CardBrandId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "CardBrandId1",
                table: "Card");

            migrationBuilder.AddColumn<string>(
                name: "MerchantCNPJ",
                table: "Transaction",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "MerchantName",
                table: "Transaction",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Card",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }
    }
}
