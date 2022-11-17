using System.Globalization;
using LocalizationSample.Middlewares;
using LocalizationSample.Service;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

builder.Services.AddScoped<ValuesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseLanguageMiddleware();

app.UseRequestLocalization(option =>
{
    option.DefaultRequestCulture = new RequestCulture(new CultureInfo("Uz"));

    option.SupportedCultures = new List<CultureInfo>()
    {   new CultureInfo("Uz"),
        new CultureInfo("Uz"),
        new CultureInfo("Ru")
    };

    option.SupportedUICultures = new List<CultureInfo>()
    {   new CultureInfo("Uz"),
        new CultureInfo("Uz"),
        new CultureInfo("Ru")
    };
    option.RequestCultureProviders = new List<IRequestCultureProvider>()
    {
        new CookieRequestCultureProvider(),
        new QueryStringRequestCultureProvider(),
        new AcceptLanguageHeaderRequestCultureProvider()
    };
});

app.UseAuthorization();

app.MapControllers();

app.Run();

