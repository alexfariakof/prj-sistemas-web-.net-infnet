using Domain.Account.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Account;
public class AdressMap : IEntityTypeConfiguration<Adress>
{
    public void Configure(EntityTypeBuilder<Adress> builder)
    {
        builder.ToTable("Adresses"); 

        builder.HasKey(a => a.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(a => a.Zipcode).HasMaxLength(20).IsRequired();
        builder.Property(a => a.Street).HasMaxLength(255).IsRequired();
        builder.Property(a => a.Number).HasMaxLength(10).IsRequired();
        builder.Property(a => a.Neighborhood).HasMaxLength(100);
        builder.Property(a => a.City).HasMaxLength(100).IsRequired();
        builder.Property(a => a.State).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Complement).HasMaxLength(100);
        builder.Property(a => a.Country).HasMaxLength(50);

    }
}
