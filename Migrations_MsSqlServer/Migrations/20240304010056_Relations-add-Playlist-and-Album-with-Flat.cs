using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RelationsaddPlaylistandAlbumwithFlat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Flat_FlatId",
                table: "Playlist");

            migrationBuilder.DropIndex(
                name: "IX_Playlist_FlatId",
                table: "Playlist");

            migrationBuilder.DropColumn(
                name: "FlatId",
                table: "Playlist");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 248, DateTimeKind.Local).AddTicks(6428),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 22, 1, 327, DateTimeKind.Local).AddTicks(9884));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 255, DateTimeKind.Local).AddTicks(1007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 22, 1, 364, DateTimeKind.Local).AddTicks(6009));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 303, DateTimeKind.Local).AddTicks(8523),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 22, 1, 412, DateTimeKind.Local).AddTicks(3176));

            migrationBuilder.CreateTable(
                name: "FlatAlbum",
                columns: table => new
                {
                    FlatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 281, DateTimeKind.Local).AddTicks(7109))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatAlbum", x => new { x.FlatId, x.AlbumId });
                    table.ForeignKey(
                        name: "FK_FlatAlbum_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlatAlbum_Flat_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlatMusic",
                columns: table => new
                {
                    FlatsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusicsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatMusic", x => new { x.FlatsId, x.MusicsId });
                    table.ForeignKey(
                        name: "FK_FlatMusic_Flat_FlatsId",
                        column: x => x.FlatsId,
                        principalTable: "Flat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlatMusic_Music_MusicsId",
                        column: x => x.MusicsId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlatPlayList",
                columns: table => new
                {
                    FlatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 310, DateTimeKind.Local).AddTicks(972))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatPlayList", x => new { x.FlatId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_FlatPlayList_Flat_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlatPlayList_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlatAlbum_AlbumId",
                table: "FlatAlbum",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatMusic_MusicsId",
                table: "FlatMusic",
                column: "MusicsId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatPlayList_PlaylistId",
                table: "FlatPlayList",
                column: "PlaylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlatAlbum");

            migrationBuilder.DropTable(
                name: "FlatMusic");

            migrationBuilder.DropTable(
                name: "FlatPlayList");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 22, 1, 327, DateTimeKind.Local).AddTicks(9884),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 248, DateTimeKind.Local).AddTicks(6428));

            migrationBuilder.AddColumn<Guid>(
                name: "FlatId",
                table: "Playlist",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 22, 1, 364, DateTimeKind.Local).AddTicks(6009),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 255, DateTimeKind.Local).AddTicks(1007));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 22, 1, 412, DateTimeKind.Local).AddTicks(3176),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 3, 22, 0, 55, 303, DateTimeKind.Local).AddTicks(8523));

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_FlatId",
                table: "Playlist",
                column: "FlatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Flat_FlatId",
                table: "Playlist",
                column: "FlatId",
                principalTable: "Flat",
                principalColumn: "Id");
        }
    }
}
