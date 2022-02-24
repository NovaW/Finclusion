using System;
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
}