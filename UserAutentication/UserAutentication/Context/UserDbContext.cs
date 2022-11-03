using System;
using UserAutentication.Models;
using Microsoft.EntityFrameworkCore;

namespace UserAutentication.Context
{
    public class UserDbContext : DbContext
    {
        DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=users.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User
            {
                Username = "1",
                Password = "1234"
            });
        }
    }
}

