using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationOnetoOneMusicBand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistPersonal_Customer_CustomerId",
                table: "PlaylistPersonal");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "PlaylistPersonal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BandId",
                table: "Music",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Music_BandId",
                table: "Music",
                column: "BandId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Music_Band_BandId",
                table: "Music",
                column: "BandId",
                principalTable: "Band",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistPersonal_Customer_CustomerId",
                table: "PlaylistPersonal",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Music_Band_BandId",
                table: "Music");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistPersonal_Customer_CustomerId",
                table: "PlaylistPersonal");

            migrationBuilder.DropIndex(
                name: "IX_Music_BandId",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Music");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "PlaylistPersonal",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistPersonal_Customer_CustomerId",
                table: "PlaylistPersonal",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
