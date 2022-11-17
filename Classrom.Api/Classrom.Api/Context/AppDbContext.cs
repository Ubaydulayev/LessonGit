using System;
using Classrom.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classrom.Api.Context;
public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
	public AppDbContext(DbContextOptions options) : base(options) { }

	public DbSet<Class> Classes { get; set; }
    public DbSet<UserClasses> UserClasses { get; set; }

}

