using Domain.Account.ValueObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Account;
public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Address");

        builder.Property(address => address.Id).HasColumnType("binary(16)")
            .HasConversion(
            v => v.ToByteArray(),
            v => new Guid(v)
            ).ValueGeneratedOnAdd();
        builder.HasKey(address => address.Id);
        builder.Property(address => address.Zipcode).HasMaxLength(20).IsRequired();
        builder.Property(address => address.Street).HasMaxLength(255).IsRequired();
        builder.Property(address => address.Number).HasMaxLength(10).IsRequired();
        builder.Property(address => address.Neighborhood).HasMaxLength(100);
        builder.Property(address => address.City).HasMaxLength(100).IsRequired();
        builder.Property(address => address.State).HasMaxLength(50).IsRequired();
        builder.Property(address => address.Complement).HasMaxLength(100);
        builder.Property(address => address.Country).HasMaxLength(50);

    }
}
