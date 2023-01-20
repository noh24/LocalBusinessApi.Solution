using Microsoft.EntityFrameworkCore;

namespace LocalBusiness.Models;

  public class ApplicationContext : DbContext
{
  public DbSet<Business> Businesses { get; set; }

  public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.Entity<Business>()
      .HasData(
        new Business { BusinessId = 1, Name = "Panda Fast", Description = "Chinese Restaurant", Review = 3 },
        new Business { BusinessId = 2, Name = "Walmarket", Description = "Supermarket", Review = 4 },
        new Business { BusinessId = 3, Name = "Tortilla Bell", Description = "Mexican Restaurant", Review = 3 },
        new Business { BusinessId = 4, Name = "Girl-Fil-A", Description = "Fast food chicken restaurant", Review = 4 },
        new Business { BusinessId = 5, Name = "Sunbucks", Description = "Coffee shop", Review = 2 },
        new Business { BusinessId = 6, Name = "Dippin Donuts", Description = "Coffee/Donut shop", Review = 2 },
        new Business { BusinessId = 8, Name = "Crushed Grapes", Description = "Wine store", Review = 1 },
        new Business { BusinessId = 9, Name = "Apple A Plus", Description = "American restaurant", Review = 5 },
        new Business { BusinessId = 7, Name = "Moonbucks", Description = "Coffee shop", Review = 5 },
        new Business { BusinessId = 10, Name = "IJump", Description = "American diner", Review = 3 },
        new Business { BusinessId = 11, Name = "House Depot", Description = "Home Improvement Company", Review = 1 },
        new Business { BusinessId = 12, Name = "Bobithy Furniture", Description = "Furniture Company", Review = 2 }
      );
  }
}
