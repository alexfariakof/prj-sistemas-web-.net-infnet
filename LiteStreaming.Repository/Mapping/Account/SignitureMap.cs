using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;

namespace Repository.Mapping.Account;
public class SignitureMap : IEntityTypeConfiguration<Signature>
{
    public void Configure(EntityTypeBuilder<Signature> builder)
    {
        builder.ToTable(nameof(Signature));

        builder.HasKey(signature => signature.Id);
        builder.Property(signature => signature.Id).ValueGeneratedOnAdd();
        builder.Property(signature => signature.Active).IsRequired();
        builder.Property(signature => signature.DtActivation).IsRequired();

        builder.HasOne(signature => signature.Flat).WithMany();
    }
}
