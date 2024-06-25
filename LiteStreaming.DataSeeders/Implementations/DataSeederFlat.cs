using Domain.Streaming.Agreggates;
using Repository;

namespace DataSeeders;
public class DataSeederFlat : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederFlat(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        _context.Flat.AddRange(
            new Flat
            {
                Name = "Free Flat",
                Description = "Free Flat Desciption",
                Value = 250m
            },

            new Flat
            {
                Name = "Basic Flat",
                Description = "Basic Flat Desciption",
                Value = 260m
            },
            new Flat
            {
                Name = "Standard  Flat",
                Description = "Standard  Flat Desciption",
                Value = 500m
            },
            new Flat
            {
                Name = "Premium  Flat",
                Description = "Premium  Flat Desciption",
                Value = 750m
            });
        try
        {
            _context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}