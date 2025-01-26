using Microsoft.EntityFrameworkCore;

namespace Pley.API.Model;

[Index(nameof(Username), IsUnique = true)]
public class Store
{
   public int Id { get; set; }
   public string? Username { get; set; }
   public string? Password { get; set; }
   public string? Name {get; set; }
   public string? Description { get; set; }
   public string? URL { get; set; }
   public List<Review> Reviews = new ();
}