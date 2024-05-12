using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Admin.ValueObject;

namespace Repository.Mapping.Admin;
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
            new Perfil(Perfil.PerfilType.Admin),
            new Perfil(Perfil.PerfilType.Normal)
        );
    }
}