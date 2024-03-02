using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FixMusicPlayListPersonalRelationsWithMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistPersonalId",
                table: "MusicPlayList");

            migrationBuilder.DropIndex(
                name: "IX_MusicPlayList_PlaylistPersonalId",
                table: "MusicPlayList");

            migrationBuilder.DropColumn(
                name: "PlaylistPersonalId",
                table: "MusicPlayList");

            migrationBuilder.CreateTable(
                name: "MusicPlayListPersonal",
                columns: table => new
                {
                    MusicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistPersonalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlayListPersonal_PlaylistPersonalId",
                table: "MusicPlayListPersonal",
                column: "PlaylistPersonalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicPlayListPersonal");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistPersonalId",
                table: "MusicPlayList",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlayList_PlaylistPersonalId",
                table: "MusicPlayList",
                column: "PlaylistPersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistPersonalId",
                table: "MusicPlayList",
                column: "PlaylistPersonalId",
                principalTable: "PlaylistPersonal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
