
namespace Pley.API.DTO;

public class LoginInDTO
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class SignUpInDTO
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class EditStoreDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? URL { get; set; }
}

