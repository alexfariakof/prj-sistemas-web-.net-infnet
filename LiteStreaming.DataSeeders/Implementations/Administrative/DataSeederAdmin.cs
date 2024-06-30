using Repository;

namespace DataSeeders.Administrative;
public class DataSeederAdmin : IDataSeederAdmin
{
    private readonly RegisterContextAdmin _context;
    public DataSeederAdmin(RegisterContextAdmin context)
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

            new DataSeederAdmin(_context).SeedData();
        }
        catch
        {
            throw;
        }
    }
}
