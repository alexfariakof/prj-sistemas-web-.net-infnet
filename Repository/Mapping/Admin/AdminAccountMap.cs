using Domain.Admin.Agreggates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping.Admin;
public class AdminAccountMap : IEntityTypeConfiguration<AdminAccount>
{
    public void Configure(EntityTypeBuilder<AdminAccount> builder)
    {
        builder.ToTable("Account");
        builder.HasKey(ac => ac.Id);
        builder.Property(ac => ac.Id).ValueGeneratedOnAdd();
        builder.Property(ac => ac.Name).IsRequired().HasMaxLength(100);        
        builder.Property(ac => ac.DtCreated).IsRequired();
    }
}
