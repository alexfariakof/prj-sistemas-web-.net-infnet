using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Core.ValueObject;
using Domain.Administrative.ValueObject;

namespace Repository.Mapping.Administrative;
public class PerfilMap : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.ToTable("Perfil");

        builder.HasKey(perfil => perfil.Id);
        builder.Property(perfil => perfil.Id).IsRequired().HasConversion<int>();
        builder.Property(perfil => perfil.Description).IsRequired();
        
        builder.HasData
        (
            new Perfil(BasePerfil.UserType.Admin),
            new Perfil(BasePerfil.UserType.Normal)
        );
    }
}