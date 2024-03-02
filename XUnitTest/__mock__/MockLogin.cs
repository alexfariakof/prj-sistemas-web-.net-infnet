using System.Security.Cryptography;
using System.Text;
using Bogus;
using Domain.Account.ValueObject;

namespace __mock__;
public class MockLogin
{
    private static MockLogin _instance;
    private static readonly object LockObject = new object();

    private readonly Faker<Login> _faker;

    private MockLogin()
    {
        _faker = new Faker<Login>()
            .RuleFor(l => l.Email, f => f.Internet.Email())
            .RuleFor(l => l.Password, f => f.Internet.Password());
    }

    public static MockLogin Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (LockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new MockLogin();
                    }
                }
            }
            return _instance;
        }
    }

    public Login GetFaker()
    {
        var fakeLogin = _faker.Generate();
        fakeLogin.Password = CryptoPassword(fakeLogin.Password ?? "");
        return fakeLogin;
    }

    private static string CryptoPassword(string openPassword)
    {
        using (SHA256 cryptoProvider = SHA256.Create())
        {
            byte[] btext = Encoding.UTF8.GetBytes(openPassword);
            var cryptoResult = cryptoProvider.ComputeHash(btext);
            return Convert.ToHexString(cryptoResult);
        }
    }
}
