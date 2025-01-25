using Pley.API.DTO;

namespace Pley.API.Model;

public class Review
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int CustomerId { get; set; }
    public required string Comment { get; set; }
    public int Rating { get; set; }
    public DateTime LastUpdated { get; set; }
    public Customer Customer { get; set; }
    public Store Store { get; set; }
}