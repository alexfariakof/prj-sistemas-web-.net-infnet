using Repository;

namespace DataSeeders.Administrative;

public class DataSeederAdministrative : IAdministrativeDataSeeder
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
            if (_context.Database.EnsureCreated())
            {
                Console.WriteLine("Banco de dados administrativo foi criado.");

                new DataSeederAdministrativeAccount(_context).SeedData();
            }
            else
            {
                Console.WriteLine("Banco de dados administrativo já existia. Não será recriado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao verificar ou criar o banco de dados administrativo: {ex.Message}");
            throw;
        }
    }
}
