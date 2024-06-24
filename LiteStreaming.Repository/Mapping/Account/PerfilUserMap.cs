using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.ValueObject;

using static Domain.Core.ValueObject.BasePerfil;

namespace Repository.Mapping.Account;
public class PerfilUserMap : IEntityTypeConfiguration<PerfilUser>
{
    public void Configure(EntityTypeBuilder<PerfilUser> builder)
    {
        builder.ToTable("PerfilUser");

        builder.HasKey(perfil => perfil.Id);
        builder.Property(perfil => perfil.Id).IsRequired().HasConversion<int>();
        builder.Property(perfil => perfil.Description).IsRequired();

        builder.HasData
        (
            new PerfilUser(UserType.Customer),
            new PerfilUser(UserType.Merchant)
        );
    }
}