using System;
using Finclusion.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Finclusion.Database.Contexts;

public class FinclusionContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Finclusion;Trusted_Connection=True;");
    }

}