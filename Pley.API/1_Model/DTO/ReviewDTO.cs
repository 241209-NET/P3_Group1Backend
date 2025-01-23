using Pley.API.Model;

namespace Pley.API.DTO;

public class ReviewInDTO
{
    public required string Comment { get; set; }
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public int Rating { get; set; }
}

public class ReviewOutDTO
{
    public required string Comment { get; set; }
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int StoreId { get; set; }
    public int Rating { get; set; }
    public DateTime Time { get; set;  }
    // public CustomerOutDTO? Customer { get; set; }
}