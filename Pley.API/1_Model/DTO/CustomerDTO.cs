using Pley.API.Model;

namespace Pley.API.DTO;

public class CustomerInDTO
{
    public int Id { get; set; } //maybe not needed for In?
    public string Name { get; set; }
}

public class CustomerOutDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double AvgRating { get; set; }
    public DateTime TIme { get; set; }

    public List<Review> Reviews { get; set; } = [];
}