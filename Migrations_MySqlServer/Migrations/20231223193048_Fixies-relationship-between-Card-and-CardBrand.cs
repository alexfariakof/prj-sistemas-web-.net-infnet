using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MySqlServer.Migrations
{
    public partial class FixiesrelationshipbetweenCardandCardBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_CardBrand_CardBrandId1",
                table: "Card");

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
                name: "CardBrandId1",
                table: "Card");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardBrandId",
                table: "Card",
                column: "CardBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_CardBrand_CardBrandId",
                table: "Card",
                column: "CardBrandId",
                principalTable: "CardBrand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_CardBrand_CardBrandId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_CardBrandId",
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
                name: "CardBrandId1",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
