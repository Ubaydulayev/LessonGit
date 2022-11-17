using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Policy.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data source=data.db");
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/api/Users/denied";
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("GetUserByIdPolicy", policyOption =>
    {
        policyOption.RequireClaim("UserAge", "21").RequireClaim("IsActive").RequireRole("Moderator");
    });

    option.AddPolicy("GetWeatherPolicy", policyOption =>
    {
        policyOption.RequireAssertion((context) =>
        {
            var isUserAge = false;
            if (int.TryParse(context.User.FindFirst("UserAge")?.Value, out var userAge))
            {
                isUserAge = userAge > 18;
            }

            return context.User.HasClaim(c => c.Type == "IsAdmin" || isUserAge);
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

