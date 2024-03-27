using Domain.Streaming.Agreggates;
using Domain.Streaming.ValueObject;
using Repository;

namespace DataSeeders.Implementations;
public class DataSeederBand : IDataSeeder
{
    private readonly RegisterContext _context;
    public DataSeederBand(RegisterContext context)
    {
        _context = context;
    }
    public void SeedData()
    {
        try
        {

            var band = new Band
            {
                Name = "Os Paralamas do Sucesso",
                Description = "Os Paralamas do Sucesso, também conhecida somente por Paralamas, é uma banda de rock brasileira formada em 1982 no município fluminense de Seropédica.",
                Backdrop = "https://playlist-music.s3.sa-east-1.amazonaws.com/Os+Paralamas+do+Sucesso/paralamas.jpg",
                Genres = _context.Genre.Where(g => g.Name == "Rock" || g.Name == "Popular").ToList(),
            };

            _context.Add(band);

            band = new Band
            {
                Name = "Legião Urbana",
                
                Description = "Legião Urbana foi uma banda brasileira de rock formada em 1982, em Brasília, por Renato Russo e Marcelo Bonfá, que contou com Dado Villa-Lobos e Renato Rocha em sua formação mais conhecida.",
                Backdrop = "https://playlist-music.s3.sa-east-1.amazonaws.com/Legi%C3%A3o+Urbana/Legi%C3%A3o_Urbana.jpg",
                Genres = _context.Genre.Where(g => g.Name == "Rock" || g.Name == "Popular").ToList(),
            };


            _context.Add(band);           
            _context.SaveChanges();
        }
        catch
        {
            Console.WriteLine($"Dados já cadastrados na base de dados");
        }
    }
}