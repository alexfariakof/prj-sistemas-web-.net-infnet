using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MySqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsFlattoCustumerMusicAlbumPlaylistswithDtAdded : Migration
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
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 907, DateTimeKind.Local).AddTicks(2226),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 231, DateTimeKind.Local).AddTicks(933));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 909, DateTimeKind.Local).AddTicks(9331),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 236, DateTimeKind.Local).AddTicks(2872));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 937, DateTimeKind.Local).AddTicks(7164),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 268, DateTimeKind.Local).AddTicks(9831));

            migrationBuilder.AddColumn<Guid>(
                name: "FlatId",
                table: "Customer",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "FlatAlbum",
                columns: table => new
                {
                    FlatId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AlbumId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 923, DateTimeKind.Local).AddTicks(8379))
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FlatMusic",
                columns: table => new
                {
                    FlatId = table.Column<Guid>(type: "char(36)", nullable: false),
                    MusicId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 932, DateTimeKind.Local).AddTicks(9285))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlatMusic", x => new { x.FlatId, x.MusicId });
                    table.ForeignKey(
                        name: "FK_FlatMusic_Flat_FlatId",
                        column: x => x.FlatId,
                        principalTable: "Flat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlatMusic_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FlatPlayList",
                columns: table => new
                {
                    FlatId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "char(36)", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 942, DateTimeKind.Local).AddTicks(6149))
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
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_FlatId",
                table: "Customer",
                column: "FlatId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatAlbum_AlbumId",
                table: "FlatAlbum",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatMusic_MusicId",
                table: "FlatMusic",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_FlatPlayList_PlaylistId",
                table: "FlatPlayList",
                column: "PlaylistId");

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

            migrationBuilder.DropTable(
                name: "FlatAlbum");

            migrationBuilder.DropTable(
                name: "FlatMusic");

            migrationBuilder.DropTable(
                name: "FlatPlayList");

            migrationBuilder.DropIndex(
                name: "IX_Customer_FlatId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "FlatId",
                table: "Customer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtCreated",
                table: "PlaylistPersonal",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 231, DateTimeKind.Local).AddTicks(933),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 907, DateTimeKind.Local).AddTicks(2226));

            migrationBuilder.AddColumn<Guid>(
                name: "FlatId",
                table: "Playlist",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayListPersonal",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 236, DateTimeKind.Local).AddTicks(2872),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 909, DateTimeKind.Local).AddTicks(9331));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DtAdded",
                table: "MusicPlayList",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 2, 19, 31, 49, 268, DateTimeKind.Local).AddTicks(9831),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 3, 3, 23, 19, 11, 937, DateTimeKind.Local).AddTicks(7164));

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
