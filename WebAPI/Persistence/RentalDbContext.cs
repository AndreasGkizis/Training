using Microsoft.EntityFrameworkCore;
using WebAPI.Domain;

namespace WebAPI.Persistence;

public class RentalDbContext : DbContext
{
    public RentalDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentalDbContext).Assembly);
        var a = new DbSeeder(modelBuilder);
        a.Seed();
    }
}

public class DbSeeder
{
    private readonly ModelBuilder _modelBuilder;

    public DbSeeder(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        _modelBuilder.Entity<Car>().HasData(
            new Car() { Id = 1, LicensePlate = "ABC123", Model = "tesla", Name = "TeslaCar" },
            new Car() { Id = 2, LicensePlate = "ass123", Model = "Lada", Name = "LadaCar" }
        );
        _modelBuilder.Entity<Customer>().HasData(
            new Customer() { Id = 1, Email = "someEmail@gmail.com", FirstName = "someFirst", LastName = "someLast" },
            new Customer() { Id = 2, Email = "someEmail1@gmail.com", FirstName = "someFirst1", LastName = "someLast1" }
        );
    }
}