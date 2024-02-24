using Domain.Account.Agreggates;
using Domain.Transactions.Agreggates;
using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Notifications;

namespace __mock__;
public static class MockMerchant
{
    public static Merchant GetFaker()
    {
        var fakeMerchant = new Faker<Merchant>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Login, MockLogin.GetFaker())
            .RuleFor(c => c.CNPJ, f => f.Company.Cnpj())
            .RuleFor(c => c.Cards, f => new List<Card>())
            .RuleFor(c => c.Signatures, f => new List<Signature>())
            .RuleFor(c => c.Notifications, f => new List<Notification>())
            .Generate();

        return fakeMerchant;
    }
}