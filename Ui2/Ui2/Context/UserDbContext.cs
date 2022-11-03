using System;
using Microsoft.EntityFrameworkCore;
using Ui2.Models;

namespace Ui2.Context;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        base.OnModelCreating(modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data source=data.db");
     
}