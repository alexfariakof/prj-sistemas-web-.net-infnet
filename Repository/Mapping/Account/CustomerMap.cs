using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Account;
public class CustomerMap : BaseAccountMap<Customer>
{
    protected override void ConfigureCustom(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(customer => customer.Birth).IsRequired();
        builder.Property(customer => customer.CPF).IsRequired().HasMaxLength(14);
        builder.OwnsOne<Phone>(Customer => Customer.Phone, prop =>
        {
            prop.Property(customer => customer.Number).HasColumnName("Phone").HasMaxLength(50).IsRequired();
            prop.WithOwner();
        });
       
        builder.HasOne(customer => customer.Flat).WithMany();
        builder.HasMany(customer => customer.Addresses).WithOne();
        builder.HasMany(customer => customer.Cards).WithOne();
        builder.HasMany(customer => customer.Signatures).WithOne();
        builder.HasMany(customer => customer.Playlists).WithOne(x => x.Customer);

    }
}
