using System;
using AuthClaims.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthClaims.Context;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        base.OnModelCreating(modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("Data source=data.db");
}
