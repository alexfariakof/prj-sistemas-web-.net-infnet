using Domain.Account.Agreggates;
using Domain.Core.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Account;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User"); 
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).ValueGeneratedOnAdd();
        builder.Property(user => user.DtCreated).ValueGeneratedOnAdd();                
        builder.HasOne(user => user.PerfilType).WithMany(perfilType => perfilType.Users).IsRequired();
        builder.OwnsOne<Login>(user => user.Login, dictonary =>
        {
            dictonary.Property(login => login.Email).HasColumnName("Email").HasMaxLength(150).IsRequired();
            dictonary.Property(login => login.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
            dictonary.WithOwner();
        });
    }
}