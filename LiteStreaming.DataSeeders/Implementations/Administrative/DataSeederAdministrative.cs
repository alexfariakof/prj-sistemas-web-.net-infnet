using Repository;

namespace DataSeeders.Administrative;
public class DataSeederAdministrative : IDataSeeder
{
    private readonly RegisterContextAdministrative _context;
    public DataSeederAdministrative(RegisterContextAdministrative context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {
            if (_context.Database.CanConnect())
            {
                Console.WriteLine("Banco de dados já existe. Não será recriado.");
                return;
            }

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            new DataSeederAdministrativeAccount(_context).SeedData();
        }
        catch
        {
            throw;
        }
    }
}
