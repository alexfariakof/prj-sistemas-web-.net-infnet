using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Transactions.ValueObject;

namespace Repository.Mapping.Transactions;
public class CreditCardBrandMap : IEntityTypeConfiguration<CreditCardBrand>
{
    public void Configure(EntityTypeBuilder<CreditCardBrand> builder)
    {
        builder.ToTable("CardBrand");
        builder.HasKey(cardbrand => cardbrand.Id);
        builder.Property(cardbrand => cardbrand.Id).IsRequired().HasConversion<int>();
        builder.Property(cardbrand => cardbrand.Name).IsRequired();                       

        builder.HasData
        (
            new CreditCardBrand((int)CardBrand.Invalid, CardBrand.Invalid.ToString()),
            new CreditCardBrand((int)CardBrand.Visa, CardBrand.Visa.ToString()),
            new CreditCardBrand((int)CardBrand.Mastercard, CardBrand.Mastercard.ToString()),
            new CreditCardBrand((int)CardBrand.Amex, CardBrand.Amex.ToString()),
            new CreditCardBrand((int)CardBrand.Discover, CardBrand.Discover.ToString()),
            new CreditCardBrand((int)CardBrand.DinersClub, CardBrand.DinersClub.ToString()),
            new CreditCardBrand((int)CardBrand.JCB, CardBrand.JCB.ToString()) 
        );
    }
}