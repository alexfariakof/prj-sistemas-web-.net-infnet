using System.Security.Cryptography;
using System.Text;
using Bogus;
using Domain.Account.ValueObject;

namespace __mock__;
public class MockLogin
{
    public static Login GetFaker()
    {
        var fakeLogin = new Faker<Login>()
            .RuleFor(l => l.Email, f => f.Internet.Email())
            .RuleFor(l => l.Password, f => f.Internet.Password())
            .Generate();

        fakeLogin.Password = CryptoPassword(fakeLogin.Password);

        return fakeLogin;
    }

    private static string CryptoPassword(string openPassword)
    {
        SHA256 cryptoProvider = SHA256.Create();
        byte[] btext = Encoding.UTF8.GetBytes(openPassword);
        var cryptoResult = cryptoProvider.ComputeHash(btext);
        return Convert.ToHexString(cryptoResult);
    }
}
