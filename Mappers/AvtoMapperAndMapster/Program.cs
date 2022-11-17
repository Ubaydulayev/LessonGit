using AvtoMapperAndMapster.Models;
using Mapster;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
TypeAdapterConfig.GlobalSettings.NewConfig<User, UserDto>()
    .BeforeMapping((user, dto) =>
    {
        user.IsActive = true;
    })
    .Map(to => to.FirstName, from => from.UserName)
    .Map(to => to.IsActiveUser, from => from.IsActive == true ? 1 : 0)
    .AfterMapping(userDto =>
    {
        if (userDto.IsActiveUser == 1)
        {
            userDto.Cost += 2000;
        }
    });

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

