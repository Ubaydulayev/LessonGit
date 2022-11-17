var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        cors.WithHeaders("Head", "X-DataCount")
        .WithMethods("https://localhost:7284", "domain")
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

app.UseAuthorization();

app.MapControllers();

app.Run();