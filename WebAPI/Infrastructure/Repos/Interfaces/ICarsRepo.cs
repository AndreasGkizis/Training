using WebAPI.Domain;

namespace WebAPI.Infrastructure.Repos.Interfaces;

public interface ICarsRepo
{
    public List<Car> GetAllCars();
    public void AddCar();
}