using WebAPI.Domain;

namespace WebAPI.Infrastructure.Repos.Interfaces;

public interface ICarsRepo
{
    public Task<List<Car>> GetAllCars(CancellationToken token =default);
    public void AddCar();
}