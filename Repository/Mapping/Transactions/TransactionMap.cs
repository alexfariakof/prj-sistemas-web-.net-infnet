using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.Agreggates;
using Domain.Core.ValueObject;

namespace Repository.Mapping.Transactions
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(Transaction));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.DtTransaction).IsRequired();
            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Customer)
                   .WithMany(cb => cb.Transactions)
                   .HasForeignKey(x => x.Id)
                   .IsRequired();

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