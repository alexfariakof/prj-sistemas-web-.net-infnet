using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Repository.Mapping.Streaming;
public abstract class MusicEntityMap<T> : IEntityTypeConfiguration<Music<T>> where T : class
{
    public void Configure(EntityTypeBuilder<Music<T>> builder)
    {
        builder.ToTable(typeof(Music<T>).Name);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.OwnsOne<Duration>(d => d.Duration, c =>
        {
            c.Property(x => x.Value).HasColumnName("Duration").IsRequired().HasMaxLength(50);
        });

        ConfigureCustom(builder);
    }

    protected abstract void ConfigureCustom(EntityTypeBuilder<Music<T>> builder);
}