using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Streaming.Agreggates;
using Domain.Core.ValueObject;

namespace Repository.Mapping.Streaming
{
    public class FlatMap : IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder.ToTable(nameof(Flat));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);

            builder.OwnsOne<Monetary>(d => d.Value, c =>
            {
                c.Property(x => x.Value)
                .HasColumnName("Monetary")
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            });
        }
    }
}