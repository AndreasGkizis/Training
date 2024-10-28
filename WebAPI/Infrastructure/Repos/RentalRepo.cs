using WebAPI.Domain;
using WebAPI.Infrastructure.Repos.Interfaces;
using WebAPI.Persistence;

namespace WebAPI.Infrastructure.Repos;

public class RentalRepo : IRentalRepo
{
    private RentalDbContext _context;

    public RentalRepo(RentalDbContext context)
    {
        _context = context;
    }

    public List<Rental> GetRentalsFroCustomer(long customerId)
    {
        return _context.Rentals.Where(r => r.CustomerId == customerId).ToList();
    }
}