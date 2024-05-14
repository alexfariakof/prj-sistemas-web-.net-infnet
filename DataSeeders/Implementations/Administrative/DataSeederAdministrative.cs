using Repository;

namespace DataSeeders.Administrative;
public class DataSeederAdministrative : IDataSeeder
{
    private readonly RegisterContextAdministravtive _context;
    public DataSeederAdministrative(RegisterContextAdministravtive context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();

            new DataSeederAdministrativeAccount(_context).SeedData();
        }
        catch
        {
            throw;
        }
    }
}
