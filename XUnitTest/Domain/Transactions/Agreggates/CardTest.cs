using Castle.Core.Resource;
using Domain.Account.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Transactions;
public class CardTest
{

    [Fact]
    public void Should_Create_Transaction_With_Success()
    {
        Card card = new Card()
        {
            Id = Guid.NewGuid(),
            Active = true,
            Limit = 1000M,
            Number = "1478-1478-1478-14787"
        };

        var customer = new Customer()
        {
            Name = "Dummy"
        };

        card.CreateTransaction(customer, 19M, "Dummy Transacao");
        Assert.True(card.Transactions.Count > 0);
        Assert.True(card.Limit == 981M);
    }

    [Fact]
    public void Should_Not_Create_Transaction_With_Inatctive_Card()
    {
        Card card = new Card()
        {
            Id = Guid.NewGuid(),
            Active = false,
            Limit = 1000M,
            Number = "1478-1478-1478-14787"
        };

        var customer = new Customer()
        {
            Name = "Dummy"
        };

        Assert.Throws<Exception>(() => card.CreateTransaction(customer, 19M, "Dummy Transacao"));
    }

    [Fact]
    public void Should_Not_Create_Transaction_With_Card_Without_Limit()
    {
        Card card = new Card()
        {
            Id = Guid.NewGuid(),
            Active = true,
            Limit = 10M,
            Number = "6465465466",
        };

        var customer = new Customer()
        {
            Name = "Dummy"
        };

        Assert.Throws<Exception>(
            () => card.CreateTransaction(customer, 19M, "Dummy Transacao"));

    }

    [Fact]
    public void Should_Not_Create_Transaction_With_Multiples_Lass_Than_2_Minutes()
    {
        Card card = new Card()
        {
            Id = Guid.NewGuid(),
            Active = true,
            Limit = 1000M,
            Number = "6465465466",
        };

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now,
            Id = Guid.NewGuid(),
            Customer = new Customer()
            {
                Name = "Dummy"
            },
            Value = 19M,
            Description = "saljasdlak"
        });

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now,
            Id = Guid.NewGuid(),
            Customer = new Customer()
            {
                Name = "Dummy"
            },
            Value = 19M,
            Description = "saljasdlak"
        });

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now,
            Id = Guid.NewGuid(),
            Customer = new Customer()
            {
                Name = "Dummy"
            },
            Value = 19M,
            Description = "saljasdlak"
        });


        var customer = new Customer()
        {
            Name = "Dummy"
        };

        Assert.Throws<Exception>(
            () => card.CreateTransaction(customer, 19M, "Dummy Transacao"));
    }

    [Fact]
    public void Should_Not_Create_Transaction_With_Duplicated_Value()
    {
        Card card = new Card()
        {
            Id = Guid.NewGuid(),
            Active = true,
            Limit = 1000M,
            Number = "6465465466",
        };

        var customer = new Customer()
        {
            Name = "Dummy"
        };
        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now,
            Id = Guid.NewGuid(),
            Customer = customer,
            Value = 19M,
            Description = "saljasdlak"
        });

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now,
            Id = Guid.NewGuid(),
            Customer = customer,
            Value = 19M,
            Description = "saljasdlak"
        });

        Assert.Throws<Exception>(
            () => card.CreateTransaction(customer, 19M, "Dummy Transacao"));
    }

    [Fact]
    public void Should_Not_Create_Transaction_With_Frequency()
    {
        Card card = new Card()
        {
            Id = Guid.NewGuid(),
            Active = true,
            Limit = 1000M,
            Number = "6465465466",
        };

        var customer = new Customer()
        {
            Name = "Dummy"
        };
        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now,
            Id = Guid.NewGuid(),
            Customer = customer,
            Value = 19M,
            Description = "saljasdlak"
        });

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now.AddMinutes(-0.5),
            Id = Guid.NewGuid(),
            Customer = customer,
            Value = 19M,
            Description = "saljasdlak"
        });


        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now.AddMinutes(-1),
            Id = Guid.NewGuid(),
            Customer = new Customer()
            {
                Name = "Dummy"
            },
            Value = 19M,
            Description = "saljasdlak"
        });

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now.AddMinutes(-4),
            Id = Guid.NewGuid(),
            Customer = new Customer()
            {
                Name = "Dummy"
            },
            Value = 19M,
            Description = "saljasdlak"
        });

        card.Transactions.Add(new Transaction()
        {
            DtTransaction = DateTime.Now.AddMinutes(-1.5),
            Id = Guid.NewGuid(),
            Customer = new Customer()
            {
                Name = "Dummy"
            },
            Value = 19M,
            Description = "saljasdlak"
        });

        Assert.Throws<Exception>(
            () => card.CreateTransaction(customer, 19M, "Dummy Transacao"));
    }
}