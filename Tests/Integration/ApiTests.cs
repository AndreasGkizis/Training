using Microsoft.Extensions.DependencyInjection;
using WebAPI.Domain;
using WebAPI.Infrastructure.Repos.Interfaces;

namespace Tests.Integration;

public class ApiTests(DatabaseFixture db) : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task Can_Get_Cars()
    {
        var repo = db.ServiceProvider.GetRequiredService<ICarsRepo>();

        var result = repo.GetAllCars();

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        
        foreach (var car in result)
        {
            car.Should().NotBeNull();
            car.Should().BeAssignableTo<Car>();
        }
    }
}