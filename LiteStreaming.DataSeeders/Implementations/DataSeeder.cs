﻿using Repository;

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
            if (_context.Database.CanConnect())
            {
                Console.WriteLine("Banco de dados já existe. Não será recriado.");
                return;
            }
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
