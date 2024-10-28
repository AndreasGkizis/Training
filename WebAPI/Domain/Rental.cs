namespace WebAPI.Domain;

public class Rental
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long CarId { get; set; }
    public long CustomerId { get; set; }
}