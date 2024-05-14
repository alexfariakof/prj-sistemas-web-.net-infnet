using Domain.Administrative.Agreggates;
using Domain.Core.ValueObject;
using Repository;

namespace DataSeeders.Administrative;
public class DataSeederAdministrativeAccount : IDataSeeder
{
    private readonly RegisterContextAdministravtive _context;

    public DataSeederAdministrativeAccount(RegisterContextAdministravtive context)
    {
        _context = context;
    }

    public void SeedData()
    {
        try
        {

            var account = new AdministrativeAccount
            {
                Name = "Admnistrador User Test ",
                Login = new Login { Email = "admin@user.com", Password = "12345T!" },
                PerfilType = _context.Perfil.Where(u => u.Id.Equals(1)).First()
            };

            _context.Add(account);

            account = new AdministrativeAccount
            {
                Name = "Normal User Test",
                Login = new Login { Email = "normal@user.com", Password = "12345T!" },
                PerfilType = _context.Perfil.Where(u => u.Id.Equals(2)).First()
            };
            _context.Add(account);
            _context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}