using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MySqlServer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseRelationsAndMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistId",
                table: "MusicPlayList");

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 231, DateTimeKind.Local).AddTicks(933),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 268, DateTimeKind.Local).AddTicks(9831),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Merchant",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MusicPlayListPersonal",
                columns: table => new
                {
                    MusicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PlaylistPersonalId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 236, DateTimeKind.Local).AddTicks(2872))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlayListPersonal", x => new { x.MusicId, x.PlaylistPersonalId });
                    table.ForeignKey(
                        name: "FK_MusicPlayListPersonal_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPlayListPersonal_PlaylistPersonal_PlaylistPersonalId",
                        column: x => x.PlaylistPersonalId,
                        principalTable: "PlaylistPersonal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Merchant_CustomerId",
                table: "Merchant",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlayListPersonal_PlaylistPersonalId",
                table: "MusicPlayListPersonal",
                column: "PlaylistPersonalId");

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

            migrationBuilder.DropTable(
                name: "MusicPlayListPersonal");

            migrationBuilder.DropIndex(
                name: "IX_Merchant_CustomerId",
                table: "Merchant");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Merchant");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 231, DateTimeKind.Local).AddTicks(933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 268, DateTimeKind.Local).AddTicks(9831));

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Merchant",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Merchant",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Merchant",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Merchant",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistId",
                table: "MusicPlayList",
                column: "PlaylistId",
                principalTable: "PlaylistPersonal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
