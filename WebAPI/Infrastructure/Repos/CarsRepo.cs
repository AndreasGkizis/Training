using Microsoft.EntityFrameworkCore;
using WebAPI.Domain;
using WebAPI.Infrastructure.Repos.Interfaces;
using WebAPI.Persistence;

namespace WebAPI.Infrastructure.Repos;

public class CarsRepo: ICarsRepo
{
    private RentalDbContext _context;

    public CarsRepo(RentalDbContext context)
    {
        _context = context;
    }

    public async Task<List<Car>> GetAllCars(CancellationToken token = default)
    {
        return await _context.Cars.ToListAsync(token); 
    }

    public void AddCar()
    {
        var car = new Car(){LicensePlate = "some license plate", Model = "some model", Name= "some color"};
        _context.Cars.Add(car);
        _context.SaveChanges();
    }
}