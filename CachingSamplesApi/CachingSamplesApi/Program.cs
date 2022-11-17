using CachingSamplesApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddDistributedSqlServerCache(option =>
{
    option.ConnectionString = "Server=sql.bsite.net\\MSSQL2016;Database=fattoev2_outlaybot;User Id=fattoev2_outlaybot;Password=outlaybot13;";
    option.SchemaName = "SchemeCache";
    option.TableName = "TableCache";
});

builder.Services.AddSingleton<DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

