using Microsoft.EntityFrameworkCore;
using Pley.API.Model;

namespace Pley.API.Data;

public class PleyContext : DbContext
{
    public PleyContext(){}
    public PleyContext(DbContextOptions<PleyContext> options) : base(options){}

    public DbSet<Store> Stores { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Customer> Customers { get; set; }
}