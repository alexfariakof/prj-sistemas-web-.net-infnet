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

            builder.HasKey(transaction => transaction.Id);
            builder.Property(transaction => transaction.Id).ValueGeneratedOnAdd();
            builder.Property(transaction => transaction.DtTransaction).IsRequired();
            builder.Property(transaction => transaction.Description).IsRequired().HasMaxLength(50);
            builder.HasOne(transaction => transaction.Customer).WithMany(cb => cb.Transactions).HasForeignKey(transaction => transaction.Id).IsRequired();
            builder.OwnsOne<Monetary>(transaction => transaction.Monetary, monetary =>
            {
                monetary.Property(transaction => transaction.Value).HasColumnName("Monetary").IsRequired().HasColumnType("decimal(18,2)");
            });
        }
    }
}