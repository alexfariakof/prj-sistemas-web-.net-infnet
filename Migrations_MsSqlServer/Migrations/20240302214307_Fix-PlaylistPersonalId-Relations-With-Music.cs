using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations_MsSqlServer.Migrations
{
    /// <inheritdoc />
    public partial class FixPlaylistPersonalIdRelationsWithMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicPlayList_PlaylistPersonal_PlaylistId",
                table: "MusicPlayList");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
