using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.ValueObject;

namespace Repository.Mapping.Account;
public class PerfilUserMap : IEntityTypeConfiguration<PerfilUser>
{
    public void Configure(EntityTypeBuilder<PerfilUser> builder)
    {
        builder.ToTable("Perfil");

        builder.HasKey(perfil => perfil.Id);
        builder.Property(perfil => perfil.Id).IsRequired().HasConversion<int>();
        builder.Property(perfil => perfil.Description).IsRequired();

        builder.HasData
        (
            new PerfilUser(PerfilUser.UserlType.Admin),
            new PerfilUser(PerfilUser.UserlType.Customer),
            new PerfilUser(PerfilUser.UserlType.Merchant)
        );
    }
}