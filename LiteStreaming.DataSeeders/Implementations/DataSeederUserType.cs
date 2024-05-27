using Domain.Account.ValueObject;
using Repository;

namespace DataSeeders;
public class DataSeederUserType : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederUserType(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {
            if (_context.PerfilUser.Count().Equals(0))
            {
                _context.PerfilUser.AddRange(
                    new PerfilUser(PerfilUser.UserType.Admin),
                    new PerfilUser(PerfilUser.UserType.Customer),
                    new PerfilUser(PerfilUser.UserType.Merchant)
                    );
            }
            _context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}