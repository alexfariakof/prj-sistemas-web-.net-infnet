using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultValueToDateCreatedPropertiesonInsertUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "User",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 514, DateTimeKind.Local).AddTicks(5419));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 507, DateTimeKind.Local).AddTicks(3310));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 512, DateTimeKind.Local).AddTicks(3707));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 582, DateTimeKind.Local).AddTicks(5563));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatPlayList",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 596, DateTimeKind.Local).AddTicks(362));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatMusic",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 573, DateTimeKind.Local).AddTicks(7200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatAlbum",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 541, DateTimeKind.Local).AddTicks(8736));

            migrationBuilder.AlterColumn<Guid>(
                name: "BandId",
                table: "Album",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 514, DateTimeKind.Local).AddTicks(5419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 507, DateTimeKind.Local).AddTicks(3310),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 512, DateTimeKind.Local).AddTicks(3707),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 582, DateTimeKind.Local).AddTicks(5563),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 596, DateTimeKind.Local).AddTicks(362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatMusic",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 573, DateTimeKind.Local).AddTicks(7200),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "FlatAlbum",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 18, 20, 17, 35, 541, DateTimeKind.Local).AddTicks(8736),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "BandId",
                table: "Album",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
