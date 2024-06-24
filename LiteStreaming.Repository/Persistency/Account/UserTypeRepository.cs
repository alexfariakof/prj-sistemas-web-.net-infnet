﻿using Domain.Account.ValueObject;
using Repository.Abstractions;
using Repository.Interfaces;

namespace Repository.Persistency.Account;
public class UserTypeRepository : BaseRepository<PerfilUser>, IRepository<PerfilUser>
{
    public RegisterContext Context { get; }
    public UserTypeRepository(RegisterContext context) : base(context)
    {
        Context = context;
    }
    public override PerfilUser GetById(int id)
    {
        return Context.Set<PerfilUser>().Find(id) ?? new();
    }
}