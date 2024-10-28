using WebAPI.Domain;

namespace WebAPI.Infrastructure.Repos.Interfaces;

public interface ICustomerRepo
{
    public List<Customer> GetAllCustomers();
}