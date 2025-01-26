
namespace Pley.API.DTO;

public class StoreInDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class StoreOutDTO
{
    public int Id { get; set; }
    public required string Username { get; set; }
}