using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsFlatCustumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 11, DateTimeKind.Local).AddTicks(5288),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 395, DateTimeKind.Local).AddTicks(275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 16, DateTimeKind.Local).AddTicks(890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 400, DateTimeKind.Local).AddTicks(8172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 58, DateTimeKind.Local).AddTicks(7134),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 455, DateTimeKind.Local).AddTicks(8946));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 65, DateTimeKind.Local).AddTicks(28),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 463, DateTimeKind.Local).AddTicks(5255));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatMusic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 53, DateTimeKind.Local).AddTicks(5843),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 427, DateTimeKind.Local).AddTicks(1165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatAlbum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 34, DateTimeKind.Local).AddTicks(9681),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 411, DateTimeKind.Local).AddTicks(9974));

            migrationBuilder.AddColumn<Guid>(
                name: "FlatId",
                table: "Customer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_FlatId",
                table: "Customer",
                column: "FlatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Flat_FlatId",
                table: "Customer",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Flat_FlatId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_FlatId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FlatId",
                table: "Customer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 395, DateTimeKind.Local).AddTicks(275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 11, DateTimeKind.Local).AddTicks(5288));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 400, DateTimeKind.Local).AddTicks(8172),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 16, DateTimeKind.Local).AddTicks(890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 455, DateTimeKind.Local).AddTicks(8946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 58, DateTimeKind.Local).AddTicks(7134));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 463, DateTimeKind.Local).AddTicks(5255),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 65, DateTimeKind.Local).AddTicks(28));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatMusic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 427, DateTimeKind.Local).AddTicks(1165),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 53, DateTimeKind.Local).AddTicks(5843));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatAlbum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 411, DateTimeKind.Local).AddTicks(9974),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 16, 44, 34, DateTimeKind.Local).AddTicks(9681));
        }
    }
}
