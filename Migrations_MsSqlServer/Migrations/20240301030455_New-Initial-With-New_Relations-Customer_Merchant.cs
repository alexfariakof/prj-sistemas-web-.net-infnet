using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class NewInitialWithNew_RelationsCustomer_Merchant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Merchant");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Merchant",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_CustomerId",
                table: "Merchant",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchant_Customer_CustomerId",
                table: "Merchant",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchant_Customer_CustomerId",
                table: "Merchant");

            migrationBuilder.DropIndex(
                name: "IX_Merchant_CustomerId",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Merchant");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Merchant",
                type: "nvarchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Merchant",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Merchant",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Merchant",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
