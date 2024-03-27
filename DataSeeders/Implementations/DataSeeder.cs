using Repository;

namespace DataSeeders.Implementations;
public class DataSeeder : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeeder(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            new DataSeederFlat(_context).SeedData();
            new DataSeederGenre(_context).SeedData();
            new DataSeederBand(_context).SeedData();
            new DataSeederUserType(_context).SeedData();
            new DataSeederCustomer(_context).SeedData();
            new DataSeederMerchant(_context).SeedData();
            new DataSeederMusic(_context).SeedData();
        }
        catch
        {
            throw;
        }
    }
}
