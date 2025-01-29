using Pley.API.Model;
using Pley.API.Util;

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
        new Customer    // 1
        {
          Name = "Homer Simpson",
          AvgRating = 0,
          URL = "https://64.media.tumblr.com/f465722bc14debe6fdc2c388f4467eb6/tumblr_pucdelTwTQ1uzae1ko1_500.gif"
        },
        new Customer    // 2
        {
          Name = "Stan Smith",
          AvgRating = 0,
          URL = "https://ih1.redbubble.net/image.1088488847.1552/st,small,507x507-pad,600x600,f8f8f8.jpg"
        },
        new Customer    // 3
        {
          Name = "Roger Smith",
          AvgRating = 0,
          URL = "https://www.toddland.com/cdn/shop/products/countryrogersticker_1024x1024.jpg?v=1595375459"
        },
        new Customer    // 4
        {
          Name = "Kenny Loggins",
          AvgRating = 0,
          URL = "https://static.tvtropes.org/pmwiki/pub/images/original_00.jpg"
        },
        new Customer    // 5
        {
          Name = "Chuck Norris",
          AvgRating = 0,
          URL = "https://ih1.redbubble.net/image.2656758435.9029/flat,750x,075,f-pad,750x1000,f8f8f8.jpg"
        },
        new Customer    // 6
        {
          Name = "Tom Selleck",
          AvgRating = 0,
          URL = "https://images.fineartamerica.com/images/artworkimages/mediumlarge/2/tom-selleck-in-magnum-p-i-1980--album.jpg"
        },
        new Customer    // 7
        {
          Name = "Pam Poovey",
          AvgRating = 0,
          URL = "https://ih1.redbubble.net/image.5396060790.4273/flat,750x,075,f-pad,750x1000,f8f8f8.jpg"
        },
        new Customer    // 8
        {
          Name = "Eric Cartman",
          AvgRating = 0,
          URL = "https://images.squarespace-cdn.com/content/v1/5d025635e9e6f00001d604a6/1561427252699-2EMQCKUQ1J49MYTDGQ47/http-%253A%253Awww.comedycentral.com.au%253Asouth-park%253Avideos%253Athe-worst-of-eric-cartman-casa-bonita-clips.jpg?format=1000w"
        },
        new Customer    // 9
        {
          Name = "Arthur Read",
          AvgRating = 0,
          URL = "https://static.wikitide.net/greatcharacterswiki/f/f8/Arthur_Read.png"
        },
        new Customer    // 10
        {
          Name = "Caillou Anderson",
          AvgRating = 0,
          URL = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/51368247-0c1e-4e48-8ba2-8bc0e076fe5c/dgabsrg-8753924b-1487-4628-a4c0-e1fe16c59190.png/v1/fill/w_1280,h_1389/angry_caillou_by_shurikenpink_dgabsrg-fullview.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9MTM4OSIsInBhdGgiOiJcL2ZcLzUxMzY4MjQ3LTBjMWUtNGU0OC04YmEyLThiYzBlMDc2ZmU1Y1wvZGdhYnNyZy04NzUzOTI0Yi0xNDg3LTQ2MjgtYTRjMC1lMWZlMTZjNTkxOTAucG5nIiwid2lkdGgiOiI8PTEyODAifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6aW1hZ2Uub3BlcmF0aW9ucyJdfQ.0tENJI8StPV6cmGBgwv66SbThiaajMWsaKtZCMd-ajw"
        },
        new Customer    // 11
        {
          Name = "Pauly D",
          AvgRating = 0,
          URL = "https://www.usmagazine.com/wp-content/uploads/2020/01/DJ-Pauly-D-Got2B-Collaboration-Promo.jpg?w=1000&quality=40&strip=all"
        },
        new Customer    // 12
        {
          Name = "Chevy Chase",
          AvgRating = 0,
          URL = "https://helios-i.mashable.com/imagery/articles/03P4X1183m0xmgqHp1Se6Cr/hero-image.fill.size_1200x1200.v1623372431.jpg"
        },
        new Customer    // 13
        {
          Name = "Robbie Rotten",
          AvgRating = 0,
          URL = "https://ichef.bbci.co.uk/news/1024/branded_news/0489/production/_96716110_lazytown2.jpg"
        },
        new Customer    // 14
        {
          Name = "Grunch",
          AvgRating = 0,
          URL = "https://i.ytimg.com/vi/z01VlftkqY8/maxresdefault.jpg"
        },
        new Customer    // 15
        {
          Name = "Justin Timberlake",
          AvgRating = 0,
          URL = "https://media.glamourmagazine.co.uk/photos/6138cc1dfe678a516c68562b/master/w_320%2Cc_limit/78383001-glamour-11may16-getty-b.jpg"
        },
        new Customer    // 16
        {
          Name = "Nicholas Cage",
          AvgRating = 0,
          URL = ""
        }
      };
      context.Customers.AddRange(customers);
      context.SaveChanges();
    }

    // Seed Stores if not already seeded
    if (!context.Stores.Any())
    {
      var stores = new Store[]
      {
        new Store     // 1
        {
          Username = "gamestop1",
          Password = "password",
          Name = "GameStop",
          Description = "Shop GameStop, the world's largest retail gaming and trade-in destination for Xbox, PlayStation, and Nintendo games, systems, consoles & accessories.",
          URL = "https://money.com/wp-content/uploads/2022/01/Investing-GameStop-Frenzy-Spencer-Jakab.jpg?quality=60&w=640"
        },
        new Store     // 2
        {
          Username = "wafflehouse1",
          Password = "password",
          Name = "Waffle House",
          Description = "Good Food Fast. Join Our Regulars Club and Get a Free Order of Hashbrowns! Sign Up Sign up for our regulars club Here. Waffle House Home Find a store near you!",
          URL = "https://locations.wafflehouse.com/wafflehouse-clipped.webp"
        },
        new Store     // 3
        {
          Username = "costco1",
          Password = "password",
          Name = "Costco",
          Description = "Costco has curated a selection of products from trusted brands such as LG, Samsung, Sony, TCL, and Hisense. We offer everything from high-resolution 4K OLED to 1080p to a $1.50 hot dog.",
          URL = "https://cdn.prod.website-files.com/610afd3581e0487e33d70566/612177cfc8b80c5327592035_Costco%20Wholesale.jpg"
        },
        new Store     // 4
        {
          Username = "starbucks1",
          Password = "password",
          Name = "Starbucks",
          Description = "Starbucks Corporation is an American multinational chain of coffeehouses and roastery reserves headquartered in Seattle, Washington.",
          URL = "https://www.usatoday.com/gcdn/authoring/authoring-images/2023/08/23/USAT/70655313007-starbucks-fall-beverages.png?crop=957,719,x241,y0"
        },
        new Store     // 5
        {
          Username = "parisbaguette1",
          Password = "password",
          Name = "Paris Baguette",
          Description = "Smiles are served daily at Paris Baguette. Enjoy delicious pastries, warm breads, stunning cakes and expertly brewed drinks while feeling right at home.",
          URL = "https://cdn.i-scmp.com/sites/default/files/d8/images/methode/2020/07/09/69ad4c1e-c19e-11ea-8c85-9f30eae6654e_image_hires_125858.jpg"
        },
        new Store     // 6
        {
          Username = "prada1",
          Password = "password",
          Name = "Prada",
          Description = "Prada S.p.A. is an Italian luxury fashion house founded in 1913 in Milan by Mario Prada. It specializes in leather handbags, travel accessories, shoes, ready-to-wear, and other fashion accessories.",
          URL = "https://media.istockphoto.com/id/487047607/photo/prada-store-in-milan-italy.jpg?s=612x612&w=0&k=20&c=PvKhPS4Ans6GEIJYf7mw4PjwGBwMSF-6rRAknGHlNOU="
        }
      };
      context.Stores.AddRange(stores);
      context.SaveChanges();
    }

    // Seed Reviews if not already seeded
    if (!context.Reviews.Any())
    {
      var reviews = new Review[]
      {
        new Review      // 1
        {
          StoreId = 5,            // Paris Baguette
          CustomerId = 1,         // Homer
          Comment = "Banned! He did not pay for the croissants!",
          Rating = 1,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 2
        {
          StoreId = 6,            // Prada
          CustomerId = 1,         // Homer
          Comment = "He has a refreshing personality.",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 3
        {
          StoreId = 1,            // GameStop 
          CustomerId = 1,         // Homer
          Comment = "Mid gamer, would not recommend.",
          Rating = 3,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 4
        {
          StoreId = 2,            // Waffle House
          CustomerId = 1,         // Homer
          Comment = "Banned!  He did not pay for his food! 😠",
          Rating = 1,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 5
        {
          StoreId = 3,            // Costco 
          CustomerId = 1,         // Homer
          Comment = "He ate hotdog in one bite.  Impressed.",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 6
        {
          StoreId = 4,            // Starbucks 
          CustomerId = 1,         // Homer
          Comment = "He ruined our bathroom.  Banned!",
          Rating = 1,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 7
        {
          StoreId = 1,            // GameStop
          CustomerId = 14,        // Grunch
          Comment = "After buying game, he gave us a dance show.",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 8
        {
          StoreId = 6,            // Prada 
          CustomerId = 11,        // Pauly D
          Comment = "He returns all the merchandise after taking pictures with them.  Terrible!",
          Rating = 1,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 9
        {
          StoreId = 5,            // Paris Baguette
          CustomerId = 6,         // Tom Selleck
          Comment = "He is a dreamy customer!!",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 10
        {
          StoreId = 2,            // Waffle House
          CustomerId = 6,         // Tom Selleck
          Comment = "He tips 15%.",
          Rating = 4,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 11
        {
          StoreId = 2,            // Waffle House
          CustomerId = 8,         // Eric Cartman
          Comment = "He was rude to our staff.",
          Rating = 2,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 12
        {
          StoreId = 3,            // Costco
          CustomerId = 5,         // Chuck Norris
          Comment = "He's our favorite customer.",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 13
        {
          StoreId = 5,            // Paris Baguette
          CustomerId = 5,         // Chuck Norris 
          Comment = "He kept demonstrating how to do a round house kick in our cafe?",
          Rating = 3,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 14
        {
          StoreId = 4,            // Starbucks
          CustomerId = 9,         // Arthur Read
          Comment = "A well-behaved young man.",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 15
        {
          StoreId = 6,            // Prada
          CustomerId = 15,        // Justin Timberlake 
          Comment = "He is a silly billy.",
          Rating = 4,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 16
        {
          StoreId = 4,            // Starbucks
          CustomerId = 15,        // Justin Timberlake 
          Comment = "He is alright, I guess.",
          Rating = 3,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 17
        {
          StoreId = 3,            // Costco 
          CustomerId = 7,         // Pam Poovey
          Comment = "She practically bought the whole store!  We love her.",
          Rating = 5,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 18
        {
          StoreId = 2,            // Waffle House
          CustomerId = 7,         // Pam Poovey
          Comment = "She's one of our regulars.",
          Rating = 4,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 19
        {
          StoreId = 3,             // Costco
          CustomerId = 12,         // Chevy Chase
          Comment = "He's a family man.",
          Rating = 4,
          LastUpdated = GenerateRandomDate()
        },
        new Review      // 20
        {
          StoreId = 2,             // Waffle House
          CustomerId = 3,          // Roger Smith
          Comment = "Messier than normal customers.",
          Rating = 2,
          LastUpdated = GenerateRandomDate()
        }
      };
      context.Reviews.AddRange(reviews);
      context.SaveChanges();

      UpdateCustomerAvgRating(context);
    }
  }

  private static void UpdateCustomerAvgRating(PleyContext context)
  {
    var utility = new Utility();
    var customers = context.Customers.ToList();

    foreach (var customer in customers)
    {
      var reviews = context.Reviews.Where(r => r.CustomerId == customer.Id).ToList();

      // using the helper function
      customer.AvgRating = utility.GetAvgRating(reviews);

      context.Customers.Update(customer);
    }

    context.SaveChanges();
  }

  private static DateTime GenerateRandomDate()
  {
    // Create a new Random instance
    var random = new Random();

    // Generate a random date within the last year
    var startDate = new DateTime(2023, 1, 1); // Start date
    var endDate = DateTime.Now; // End date (current date)

    var range = endDate - startDate;
    var randomTimeSpan = new TimeSpan((long)(random.NextDouble() * range.Ticks));

    return startDate + randomTimeSpan; // Returns a random date between startDate and endDate
  }
}
