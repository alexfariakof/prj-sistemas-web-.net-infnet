using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;

namespace Repository.Mapping.Account;
public class MerchantMap : BaseAccountMap<Merchant>
{
    protected override void ConfigureCustom(EntityTypeBuilder<Merchant> builder)
    {
        builder.Property(x => x.CNPJ).IsRequired().HasMaxLength(18);
    
        builder.OwnsOne<Phone>(e => e.Phone, c =>
        {
            c.Property(x => x.Number).HasColumnName("Phone").HasMaxLength(50).IsRequired();

        });

        builder.OwnsOne<Login>(e => e.Login, c =>
        {
            c.Property(x => x.Email).HasColumnName("Email").HasMaxLength(150).IsRequired();
            c.Property(x => x.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
        });

        builder.HasMany(x => x.Addresses).WithOne();
        builder.HasMany(x => x.Cards).WithOne();
        builder.HasMany(x => x.Signatures).WithOne();
    }
}
