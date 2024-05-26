using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;

namespace Repository.Mapping.Account;
public class MerchantMap : BaseAccountMap<Merchant>
{
    protected override void ConfigureCustom(EntityTypeBuilder<Merchant> builder)
    {
        builder.Property(merchant => merchant.CNPJ).IsRequired().HasMaxLength(18);
        builder.HasOne(merchant => merchant.Customer).WithOne().OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(merchant => merchant.Addresses).WithOne();
        builder.HasMany(merchant => merchant.Cards).WithOne();
        builder.HasMany(merchant => merchant.Signatures).WithOne();
    }
}
