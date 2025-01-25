using Pley.API.Model;

namespace Pley.API.Data;


public static class DbInitializer
{
  public static void Initialize(PleyContext context)
  {
    context.Database.EnsureCreated();

    // Seed Customers if not already seeded
    if (!context.Customers.Any())
    {
      var customers = new Customer[]
      {
                  new Customer { Name = "John Doe", Email = "john.doe@example.com" },
                  new Customer { Name = "Jane Smith", Email = "jane.smith@example.com" }
      };
      context.Customers.AddRange(customers);
      context.SaveChanges();
    }

    // Seed Stores if not already seeded
    if (!context.Stores.Any())
    {
      var stores = new Store[]
      {
                  new Store { Name = "Tech Store", Location = "New York" },
                  new Store { Name = "Fashion Boutique", Location = "Los Angeles" }
      };
      context.Stores.AddRange(stores);
      context.SaveChanges();
    }

    // Seed Reviews if not already seeded
    if (!context.Reviews.Any())
    {
      var reviews = new Review[]
      {
                  new Review { Rating = 5, Comment = "Great store!", CustomerId = 1, StoreId = 1 },
                  new Review { Rating = 4, Comment = "Good experience.", CustomerId = 2, StoreId = 2 }
      };
      context.Reviews.AddRange(reviews);
      context.SaveChanges();
    }
  }
}
