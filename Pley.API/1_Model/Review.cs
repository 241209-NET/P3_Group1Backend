namespace Pley.API.Model;

public class Review
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int CustomerId { get; set; }
    public required string Comment { get; set; }
    public DateTime Time { get; set; }
    public int Rating { get; set; }

    public Review()
    {    
    }
}