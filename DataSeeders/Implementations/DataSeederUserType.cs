using Domain.Core.ValueObject;
using Repository;

namespace DataSeeders.Implementations;
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
            if (_context.Perfil.Count().Equals(0))
            {
                _context.Perfil.AddRange(
                    new Perfil(Perfil.PerfilType.Admin),
                    new Perfil(Perfil.PerfilType.Customer),
                    new Perfil(Perfil.PerfilType.Merchant)
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