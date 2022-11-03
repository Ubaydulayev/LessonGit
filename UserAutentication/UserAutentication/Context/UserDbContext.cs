using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAutentication.Models;

namespace UserAutentication.Context;
public class UserDbContext : DbContext
{

    public UserDbContext(DbContextOptions<UserDbContext> options) : base( options) { }

    DbSet<User> Users { get; set; }
    public UnauthorizedResult Result { get; internal set; }
    public object HttpContext { get; internal set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.Entity<User>()

        //    .HasNoKey()
        //    .ToTable("User");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite("DataSource=data.db");
    

}

// https://go.microsoft.com/fwlink/?linkid=2141943.

//No database provider has been configured for this DbContext.A provider can be configured by overriding the 'DbContext.OnConfiguring' method or by using 'AddDbContext' on the application service provider.If 'AddDbContext' is used, then also ensure that your DbContext type accepts a DbContextOptions<TContext> object in its constructor and passes it to the base constructor for DbContext.
//The entity type 'User' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'