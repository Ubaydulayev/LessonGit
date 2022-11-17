using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PolicyAndCorsApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
}).AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("GetUserByIdPolicy", policyOption =>
    {
        policyOption.RequireClaim("UserAge", "21").RequireClaim("IsActive").RequireRole("moderator");
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

builder.Services.AddCors(option =>
{
    option.AddPolicy("All", cors =>
    {
        cors.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });

    option.AddPolicy("Front", cors =>
    {
        cors.WithHeaders("Head") //shunaqa yozsa ham bo`ladi "X-DataCount"
        .WithMethods("https://localhost:7285", "domain")
        .WithOrigins("POST");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("All");

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

