using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Core.ValueObject;

namespace Repository.Mapping.Account;
public class PerfilMap : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.ToTable("Perfil");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().HasConversion<int>();
        builder.Property(x => x.Description).IsRequired();

        builder.HasData
        (
            new Perfil(Perfil.PerfilType.Admin),
            new Perfil(Perfil.PerfilType.Customer),
            new Perfil(Perfil.PerfilType.Merchant)
        );
    }
}