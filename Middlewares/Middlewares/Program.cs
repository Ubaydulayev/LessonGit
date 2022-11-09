using Middlewares.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<LanguageMiddleware>();
app.MapControllers();

app.Run();
