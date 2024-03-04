using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddDtAddedtoFlatMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlatMusic_Flat_FlatsId",
                table: "FlatMusic");

            migrationBuilder.DropForeignKey(
                name: "FK_FlatMusic_Music_MusicsId",
                table: "FlatMusic");

            migrationBuilder.RenameColumn(
                name: "MusicsId",
                table: "FlatMusic",
                newName: "MusicId");

            migrationBuilder.RenameColumn(
                name: "FlatsId",
                table: "FlatMusic",
                newName: "FlatId");

            migrationBuilder.RenameIndex(
                name: "IX_FlatMusic_MusicsId",
                table: "FlatMusic",
                newName: "IX_FlatMusic_MusicId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 395, DateTimeKind.Local).AddTicks(275),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 248, DateTimeKind.Local).AddTicks(6428));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 400, DateTimeKind.Local).AddTicks(8172),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 255, DateTimeKind.Local).AddTicks(1007));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 455, DateTimeKind.Local).AddTicks(8946),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 303, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 463, DateTimeKind.Local).AddTicks(5255),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 310, DateTimeKind.Local).AddTicks(972));

            migrationBuilder.AddColumn<DateTime>(
                name: "DtAdded",
                table: "FlatMusic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 427, DateTimeKind.Local).AddTicks(1165));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatAlbum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 411, DateTimeKind.Local).AddTicks(9974),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 281, DateTimeKind.Local).AddTicks(7109));

            migrationBuilder.AddForeignKey(
                name: "FK_FlatMusic_Flat_FlatId",
                table: "FlatMusic",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlatMusic_Music_MusicId",
                table: "FlatMusic",
                column: "MusicId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlatMusic_Flat_FlatId",
                table: "FlatMusic");

            migrationBuilder.DropForeignKey(
                name: "FK_FlatMusic_Music_MusicId",
                table: "FlatMusic");

            migrationBuilder.DropColumn(
                name: "DtAdded",
                table: "FlatMusic");

            migrationBuilder.RenameColumn(
                name: "MusicId",
                table: "FlatMusic",
                newName: "MusicsId");

            migrationBuilder.RenameColumn(
                name: "FlatId",
                table: "FlatMusic",
                newName: "FlatsId");

            migrationBuilder.RenameIndex(
                name: "IX_FlatMusic_MusicId",
                table: "FlatMusic",
                newName: "IX_FlatMusic_MusicsId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 248, DateTimeKind.Local).AddTicks(6428),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 395, DateTimeKind.Local).AddTicks(275));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 255, DateTimeKind.Local).AddTicks(1007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 400, DateTimeKind.Local).AddTicks(8172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 303, DateTimeKind.Local).AddTicks(8523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 455, DateTimeKind.Local).AddTicks(8946));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 310, DateTimeKind.Local).AddTicks(972),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 463, DateTimeKind.Local).AddTicks(5255));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatAlbum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 281, DateTimeKind.Local).AddTicks(7109),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 6, 34, 411, DateTimeKind.Local).AddTicks(9974));

            migrationBuilder.AddForeignKey(
                name: "FK_FlatMusic_Flat_FlatsId",
                table: "FlatMusic",
                column: "FlatsId",
                principalTable: "Flat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlatMusic_Music_MusicsId",
                table: "FlatMusic",
                column: "MusicsId",
                principalTable: "Music",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
