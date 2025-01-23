namespace Pley.API.Model;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double AvgRating { get; set; }

    public Customer()
    {
    }
}