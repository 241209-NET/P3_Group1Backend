namespace Pley.API.Model;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double AvgRating { get; set; }
    public string? URL { get; set; }
    public List<Review> Reviews = new ();
}