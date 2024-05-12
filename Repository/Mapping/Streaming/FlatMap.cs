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

            builder.HasKey(flat => flat.Id);
            builder.Property(flat => flat.Id).ValueGeneratedOnAdd();
            builder.Property(flat => flat.Name).IsRequired().HasMaxLength(50);
            builder.Property(flat => flat.Description).IsRequired().HasMaxLength(1024);

            builder.OwnsOne<Monetary>(d => d.Value, c =>
            {
                c.Property(flat => flat.Value)
                .HasColumnName("Monetary")
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            });
        }
    }
}