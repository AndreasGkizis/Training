using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Repos;
using WebAPI.Infrastructure.Repos.Interfaces;
using WebAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RentalDbContext>(opt =>
{
    opt.UseSqlServer(
        builder.Configuration.GetConnectionString("RentalDB"),
        mi => mi.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)
    );
});

builder.Services.AddTransient<IRentalRepo, RentalRepo>();
builder.Services.AddTransient<ICustomerRepo, CustomerRepo>();
builder.Services.AddTransient<ICarsRepo, CarsRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<RentalDbContext>();
    await context.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.MapGet("/getCars", (ICarsRepo repo) => repo.GetAllCars())
    .WithName("Get Cars")
    .WithOpenApi();

app.MapPost("/addCars", (ICarsRepo repo) => repo.AddCar())
    .WithName("Add Cars")
    .WithOpenApi();

app.MapGet("/getCustomers", (ICustomerRepo repo) => repo.GetAllCustomers())
    .WithName("Get Customers")
    .WithOpenApi();

app.MapGet("/rental/{customerId:int}",
        (RentalDbContext context, int customerId) =>
            context.Rentals.Where(ren => ren.CustomerId == customerId).ToList())
    .WithName("Get Rentals")
    .WithOpenApi();

app.Run();