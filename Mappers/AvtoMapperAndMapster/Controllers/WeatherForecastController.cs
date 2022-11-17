using Microsoft.AspNetCore.Mvc;
using AvtoMapperAndMapster.Models;
using Mapster;
using AutoMapper;

namespace AvtoMapperAndMapster.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private User UserObject = new User()
    {
        Id = 1,
        Name = "Sad",
        Email = "123@gma.co",
        Password = "1234",
        UserName = "Berdi",
        IsActive = true,
        Cost = 2000
    };

    //Mapster
    [HttpGet]
    public IActionResult GetResult()
    {
        var config = new TypeAdapterConfig();

        config.NewConfig<User, UserDto>()
            .BeforeMapping((user, dto) =>
            {
                user.IsActive = false;
            })
            .Map(to => to.FirstName, from => from.UserName)
            .Map(to => to.IsActiveUser, from => from.IsActive == true ? 1 : 0)
            .AfterMapping(userDto =>
            {
                if(userDto.IsActiveUser == 0)
                {
                    userDto.Cost += 5000;
                }
            });

        UserDto dto = UserObject.Adapt<UserDto>(config);

        //global settings
        // UserDto dto = UserObject.Adapt<UserDto>();

        return Ok(dto);
    }

    //AutoMapster
    [HttpGet]
    public IActionResult GetAuto()
    {
        var configuration = new MapperConfiguration(cfg =>
        cfg.CreateMap<User, UserDto>().ForMember(to => to.FirstName, user =>
        {
            user.MapFrom(from => from.UserName);
        })).CreateMapper();

        UserDto dto = configuration.Map<UserDto>(UserObject);

        //global settings
        // UserDto dto = UserObject.Adapt<UserDto>();

        return Ok(dto);
    }
}

