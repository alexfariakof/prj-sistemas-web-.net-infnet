using Domain.Streaming.Agreggates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mapping.Streaming
{
    public class AlbumMap : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable(nameof(Album));

            builder.HasKey(album => album.Id);
            builder.Property(album => album.Id).ValueGeneratedOnAdd();
            builder.Property(album => album.Name).IsRequired().HasMaxLength(50);
            builder.Property(album => album.Backdrop).IsRequired();
            builder.HasMany(album => album.Musics).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(album => album.Genres).WithMany(m => m.Albums);

            builder.HasMany(album => album.Flats)
                .WithMany(album => album.Albums)
                .UsingEntity<Dictionary<string, object>>(
                "FlatAlbum",
                j => j
                .HasOne<Flat>()
                .WithMany()
                .HasForeignKey("FlatId"),
                j => j
                .HasOne<Album>()
                .WithMany()
                .HasForeignKey("AlbumId"),
                j =>
                {
                    j.HasKey("FlatId", "AlbumId");
                    // Funciona com Sqlserver não deixando o campo nulo 
                    // j.Property<DateTime?>("DtAdded").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd(); 
                    j.Property<DateTime?>("DtAdded").ValueGeneratedOnAdd();
                });
        }
    }
 }
