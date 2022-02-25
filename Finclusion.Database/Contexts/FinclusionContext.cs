using Finclusion.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Finclusion.Database.Contexts;

public class FinclusionContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public FinclusionContext(DbContextOptions<FinclusionContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
              .HasData(
               new Product { Id = 1, ProductName = "Chair", Cost = 50, Quantity = 7},
               new Product { Id = 2, ProductName = "Table", Cost = 150, Quantity = 4},
               new Product { Id = 3, ProductName = "Fridge", Cost = 230, Quantity = 2}
               );
    }
}