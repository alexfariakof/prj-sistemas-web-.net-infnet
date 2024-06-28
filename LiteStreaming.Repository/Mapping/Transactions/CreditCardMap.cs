using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.Agreggates;
using Domain.Core.ValueObject;
using Domain.Transactions.ValueObject;

namespace Repository.Mapping.Transactions;

public class CreditCardMap : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable(nameof(Card));      
        builder.Property(card => card.Id).HasColumnType("binary(16)")
            .HasConversion(
            v => v.ToByteArray(),
            v => new Guid(v)
            )
            .ValueGeneratedOnAdd();
        builder.HasKey(card => card.Id);
        builder.Property(card => card.Active).IsRequired();
        builder.Property(card => card.Number).IsRequired().HasMaxLength(19);            
        builder.Property(card => card.CVV).IsRequired().HasMaxLength(255);
        builder.HasOne(card => card.CardBrand).WithMany(card => card.Cards).IsRequired();


        builder.OwnsOne<ExpiryDate>(e => e.Validate, (Action<OwnedNavigationBuilder<Card, ExpiryDate>>)(expiryDate =>
        {
            expiryDate.Ignore(e => e.Month);
            expiryDate.Ignore(e => e.Year);
            expiryDate.Property(card => card.Value).HasColumnName("Validate").IsRequired();
        }));

        builder.OwnsOne<Monetary>(card => card.Limit, monetary =>
        {
            monetary.Property(card => card.Value).HasColumnName("Limit").IsRequired().HasColumnType("decimal(18,2)");
        });

        builder.HasMany(card => card.Transactions).WithOne();

    }
}