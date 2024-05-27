﻿using Domain.Account.Agreggates;
using Domain.Account.ValueObject;
using Repository.Abastractions;
using Repository.Interfaces;

namespace Repository.Persistency.Account;
public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
{
    private new RegisterContext Context { get; set; }
    public CustomerRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }

    public override void Save(Customer entity)
    {
        entity.User.PerfilType = Context.Set<PerfilUser>().Find(entity.User.PerfilType.Id);
        Context.Add(entity);
        Context.SaveChanges();
    }

    public override void Update(Customer entity)
    {
        entity.User.PerfilType = Context.Set<PerfilUser>().Find(entity.User.PerfilType.Id);
        Context.Update(entity);
        Context.SaveChanges();
    }
}