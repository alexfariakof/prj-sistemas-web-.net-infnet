﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Account.Agreggates;

namespace Repository.Mapping.Account;
public abstract class BaseAccountMap<T> : IEntityTypeConfiguration<T> where T : AbstractAccount<T>
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(typeof(T).Name);        
        builder.HasKey(account => account.Id);
        builder.Property(account => account.Id).ValueGeneratedOnAdd();
        builder.Property(account => account.Name).IsRequired().HasMaxLength(100);
        builder.HasOne(account => account.User).WithMany().OnDelete(DeleteBehavior.NoAction);
        ConfigureCustom(builder);
    }
    protected abstract void ConfigureCustom(EntityTypeBuilder<T> builder);
}