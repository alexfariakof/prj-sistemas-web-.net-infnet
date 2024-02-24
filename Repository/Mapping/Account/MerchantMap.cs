using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;
using Domain.Account.ValueObject;

namespace Repository.Mapping.Account
{
    public class MerchantMap : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> builder)
        {
            builder.ToTable(nameof(Merchant));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CNPJ).IsRequired().HasMaxLength(18);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.OwnsOne<Phone>(e => e.Phone, c =>
            {
                c.Property(x => x.Number).HasColumnName("Phone").HasMaxLength(50).IsRequired();

            });

            builder.OwnsOne<Login>(e => e.Login, c =>
            {
                c.Property(x => x.Email).HasColumnName("Email").HasMaxLength(150).IsRequired();
                c.Property(x => x.Password).HasColumnName("Password").HasMaxLength(255).IsRequired();
            });

            builder.HasMany(x => x.Cards).WithOne();
            builder.HasMany(x => x.Signatures).WithOne();
        }
    }
}
