﻿using Domain.Account.ValueObject;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates;
public class Merchant : AbstractAccount<Merchant>
{
    public string? CNPJ { get; set; }
    public Phone? Phone { get; set; }
    public virtual IList<Transaction>? Transactions { get; set; }
    public override void CreateAccount(Merchant merchant, Address address,  Flat flat, Card card)
    {
        Name = merchant.Name;
        CNPJ = merchant.CNPJ;
        Phone = merchant.Phone;
        Customer customer = new Customer()
        {
            Id = merchant.Id,
            Name = merchant.Name,
            CPF = merchant.CPF,
            Phone = merchant.Phone
        };
        AddAdress(address);
        AddFlat(customer, flat, card);
        AddCard(card);
    }
}
