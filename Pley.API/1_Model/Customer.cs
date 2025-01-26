namespace Pley.API.Model;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }

    private double _AvgRating;
    public double AvgRating 
    { 
        get
        {
            return _AvgRating;
        }
        set
        {
            _AvgRating = Math.Round(_AvgRating, 1);
            _AvgRating =value;
        } 
    }
    public string? URL { get; set; }
    public List<Review> Reviews = new ();
}