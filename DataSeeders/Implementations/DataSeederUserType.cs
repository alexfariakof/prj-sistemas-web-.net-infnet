using Domain.Account.ValueObject;
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
            if (_context.UserType.Count().Equals(0))
            {
                _context.UserType.AddRange(
                    new UserType(UserTypeEnum.Admin) { Description = "Admin" },
                    new UserType(UserTypeEnum.Customer) { Description = "Customer" },
                    new UserType(UserTypeEnum.Merchant) { Description = "Merchant" }
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