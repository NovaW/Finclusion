using System;
using Finclusion.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Finclusion.Database.Contexts;

public class FinclusionContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Finclusion;Trusted_Connection=True;");
    }

}