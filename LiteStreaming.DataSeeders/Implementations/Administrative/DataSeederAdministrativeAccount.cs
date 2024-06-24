using Domain.Administrative.Agreggates;
using Domain.Administrative.ValueObject;
using Domain.Core.ValueObject;
using Repository;

namespace DataSeeders.Administrative;
public class DataSeederAdministrativeAccount : IDataSeeder
{
    private readonly RegisterContextAdministrative _context;

    public DataSeederAdministrativeAccount(RegisterContextAdministrative context)
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
                PerfilType = _context.Perfil.Where(u => u.Id.Equals((int)Perfil.UserType.Admin)).First()
            };

            _context.Add(account);

            account = new AdministrativeAccount
            {
                Name = "Normal User Test",
                Login = new Login { Email = "normal@user.com", Password = "12345T!" },
                PerfilType = _context.Perfil.Where(u => u.Id.Equals((int)Perfil.UserType.Normal)).First()
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