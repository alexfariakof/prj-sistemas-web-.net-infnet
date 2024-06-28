using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.MySqlServer.Migrations.Application
{
    /// <inheritdoc />
    public partial class ConvertPKsTypeGUIDtoBinary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "User",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MerchantId",
                table: "Transaction",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CardId",
                table: "Transaction",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Transaction",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MerchantId",
                table: "Signature",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "FlatId",
                table: "Signature",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerId",
                table: "Signature",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Signature",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerId",
                table: "PlaylistPersonal",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "AlbumId",
                table: "PlaylistPersonal",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "PlaylistPersonal",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Playlist",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "SenderId",
                table: "Notification",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "MerchantId",
                table: "Notification",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "DestinationId",
                table: "Notification",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Notification",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PlaylistPersonalId",
                table: "MusicPlayListPersonal",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MusicId",
                table: "MusicPlayListPersonal",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PlaylistsId",
                table: "MusicPlayList",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MusicsId",
                table: "MusicPlayList",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "BandId",
                table: "Music",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "AlbumId",
                table: "Music",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Music",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UserId",
                table: "Merchant",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerId",
                table: "Merchant",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Merchant",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PlaylistsId",
                table: "GenrePlaylist",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "GenresId",
                table: "GenrePlaylist",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MusicsId",
                table: "GenreMusic",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "GenresId",
                table: "GenreMusic",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Genre",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PlaylistsId",
                table: "FlatPlayList",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FlatsId",
                table: "FlatPlayList",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MusicsId",
                table: "FlatMusic",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FlatsId",
                table: "FlatMusic",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "AlbumId",
                table: "FlatAlbum",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FlatId",
                table: "FlatAlbum",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Flat",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "UserId",
                table: "Customer",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "FlatId",
                table: "Customer",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Customer",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MerchantId",
                table: "Card",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerId",
                table: "Card",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Card",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "GenresId",
                table: "BandGenre",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "BandsId",
                table: "BandGenre",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Band",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "GenresId",
                table: "AlbumGenre",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "AlbumsId",
                table: "AlbumGenre",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "BandId",
                table: "Album",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Album",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "MerchantId",
                table: "Address",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "CustomerId",
                table: "Address",
                type: "binary(16)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Id",
                table: "Address",
                type: "binary(16)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "User",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MerchantId",
                table: "Transaction",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CardId",
                table: "Transaction",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Transaction",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MerchantId",
                table: "Signature",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FlatId",
                table: "Signature",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Signature",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Signature",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "PlaylistPersonal",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumId",
                table: "PlaylistPersonal",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "PlaylistPersonal",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Playlist",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Notification",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MerchantId",
                table: "Notification",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinationId",
                table: "Notification",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Notification",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaylistPersonalId",
                table: "MusicPlayListPersonal",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MusicId",
                table: "MusicPlayListPersonal",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaylistsId",
                table: "MusicPlayList",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MusicsId",
                table: "MusicPlayList",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BandId",
                table: "Music",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumId",
                table: "Music",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Music",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Merchant",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Merchant",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Merchant",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaylistsId",
                table: "GenrePlaylist",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenresId",
                table: "GenrePlaylist",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MusicsId",
                table: "GenreMusic",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenresId",
                table: "GenreMusic",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Genre",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaylistsId",
                table: "FlatPlayList",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FlatsId",
                table: "FlatPlayList",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MusicsId",
                table: "FlatMusic",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FlatsId",
                table: "FlatMusic",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumId",
                table: "FlatAlbum",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FlatId",
                table: "FlatAlbum",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Flat",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Customer",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FlatId",
                table: "Customer",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Customer",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MerchantId",
                table: "Card",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Card",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Card",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenresId",
                table: "BandGenre",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BandsId",
                table: "BandGenre",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Band",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "GenresId",
                table: "AlbumGenre",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlbumsId",
                table: "AlbumGenre",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "BandId",
                table: "Album",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Album",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");

            migrationBuilder.AlterColumn<Guid>(
                name: "MerchantId",
                table: "Address",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Address",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Address",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "binary(16)");
        }
    }
}
