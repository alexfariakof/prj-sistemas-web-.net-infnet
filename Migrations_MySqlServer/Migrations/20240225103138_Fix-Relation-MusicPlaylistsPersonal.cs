using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MySqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationMusicPlaylistsPersonal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Music_PlaylistPersonal_PlaylistPersonalId",
                table: "Music");

            migrationBuilder.DropIndex(
                name: "IX_Music_PlaylistPersonalId",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "PlaylistPersonalId",
                table: "Music");

            migrationBuilder.AddForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistId",
                table: "MusicPlayList",
                column: "PlaylistId",
                principalTable: "PlaylistPersonal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistId",
                table: "MusicPlayList");

            migrationBuilder.AddColumn<Guid>(
                name: "PlaylistPersonalId",
                table: "Music",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Music_PlaylistPersonalId",
                table: "Music",
                column: "PlaylistPersonalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Music_PlaylistPersonal_PlaylistPersonalId",
                table: "Music",
                column: "PlaylistPersonalId",
                principalTable: "PlaylistPersonal",
                principalColumn: "Id");
        }
    }
}
