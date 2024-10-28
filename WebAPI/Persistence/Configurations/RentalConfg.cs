using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Domain;

namespace WebAPI.Persistence.Configurations;
internal class RentalConfg : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne<Customer>().WithMany().HasForeignKey(col=>col.CustomerId);
        builder.HasOne<Car>().WithMany().HasForeignKey(col=>col.CarId);
    }
}