using WebAPI.Domain;

namespace WebAPI.Infrastructure.Repos.Interfaces;

public interface IRentalRepo
{
    public List<Rental> GetRentalsFroCustomer(long customerId);
}