
namespace Pley.API.DTO;

// public class ReviewInDTO
// {
//     public required string Comment { get; set; }
//     public int CustomerId { get; set; }
//     public int StoreId { get; set; }
//     public int Rating { get; set; }
// }
public class ReviewInDTO
{
    public int? Rating { get; set; }
    public string? Comment { get; set; }
}