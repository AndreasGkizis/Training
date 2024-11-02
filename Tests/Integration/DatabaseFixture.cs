using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using WebAPI.Infrastructure.Repos;
using WebAPI.Infrastructure.Repos.Interfaces;
using WebAPI.Persistence;

namespace Tests.Integration;

public class DatabaseFixture : IAsyncLifetime
{
    public readonly IServiceProvider ServiceProvider;
    private readonly MsSqlContainer _container;

    public DatabaseFixture()
    {
        _container = new MsSqlBuilder().Build();
        ServiceProvider = RegisterServices();
    }

    private ServiceProvider RegisterServices()
    {
        var coll = new ServiceCollection();

        coll.AddTransient<IConfiguration>(_ =>
        {
            var configs = new List<KeyValuePair<string, string?>>
            {
                new("ConnectionStrings:RentalDB", _container.GetConnectionString())
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(configs)
                .Build();
        });

        coll.AddDbContext<RentalDbContext>( opt=> 
            opt.UseSqlServer(
                _container.GetConnectionString()
                )
        );
        coll.AddTransient<IRentalRepo, RentalRepo>();
        coll.AddTransient<ICustomerRepo, CustomerRepo>();
        coll.AddTransient<ICarsRepo, CarsRepo>();

        return coll.BuildServiceProvider();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        var dbContext = ServiceProvider.GetRequiredService<RentalDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}