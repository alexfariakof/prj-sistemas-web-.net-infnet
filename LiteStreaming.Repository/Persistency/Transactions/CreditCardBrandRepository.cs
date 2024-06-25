﻿using Domain.Transactions.ValueObject;
using Repository.Abstractions;
using Repository.Interfaces;

namespace Repository.Persistency.Transactions;
public class CreditCardBrandRepository : BaseRepository<CreditCardBrand>, IRepository<CreditCardBrand>
{
    public RegisterContext Context { get; }
    public CreditCardBrandRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override CreditCardBrand GetById(int id)
    {
        return Context.Set<CreditCardBrand>().Find(id) ?? new();
    }
}