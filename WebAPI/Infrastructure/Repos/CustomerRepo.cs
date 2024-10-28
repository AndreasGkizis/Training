using WebAPI.Domain;
using WebAPI.Infrastructure.Repos.Interfaces;
using WebAPI.Persistence;

namespace WebAPI.Infrastructure.Repos;

public class CustomerRepo(RentalDbContext context) : ICustomerRepo
{
    private RentalDbContext _context = context;

    public List<Customer> GetAllCustomers() => _context.Customers.ToList();
}