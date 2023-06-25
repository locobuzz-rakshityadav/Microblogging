using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FollowAPI.Models;

public class FeedDbContext : DbContext
{
    public DbSet<Feed> Feed{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your_SQL_Server_Connection_String");
    }
}