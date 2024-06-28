using Repository;

namespace DataSeeders;

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
            if (_context.Database.EnsureCreated())
            {
                Console.WriteLine("Banco de dados foi criado.");

                new DataSeederFlat(_context).SeedData();
                new DataSeederGenre(_context).SeedData();
                new DataSeederBand(_context).SeedData();
                new DataSeederUserType(_context).SeedData();
                new DataSeederCustomer(_context).SeedData();
                new DataSeederMerchant(_context).SeedData();
                new DataSeederMusic(_context).SeedData();
            }
            else
            {
                Console.WriteLine("Banco de dados já existe. Não será recriado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar ou criar o banco de dados: {ex.Message}");
            throw;
        }
    }
}
