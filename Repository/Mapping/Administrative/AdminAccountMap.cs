using Domain.Administrative.Agreggates;
using Domain.Core.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Administrative;
public class AdminAccountMap : IEntityTypeConfiguration<AdministrativeAccount>
{
    public void Configure(EntityTypeBuilder<AdministrativeAccount> builder)
    {
        builder.ToTable("Account");
        builder.HasKey(account => account.Id);
        builder.Property(account => account.Id).ValueGeneratedOnAdd();
        builder.Property(account => account.Name).IsRequired().HasMaxLength(100);        
        builder.Property(account => account.DtCreated).IsRequired();
        builder.HasOne(account => account.PerfilType).WithMany(perfil => perfil.Users).IsRequired();
        builder.OwnsOne<Login>(account => account.Login, login =>
        {
            login.Property(prop => prop.Email).HasColumnName("Email").HasMaxLength(150).IsRequired();
            login.Property(prop => prop.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
            login.WithOwner();
        });
    }
}
