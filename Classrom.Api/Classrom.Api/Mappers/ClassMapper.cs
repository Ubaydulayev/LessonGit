using System;
using Classrom.Api.Entities;
using Classrom.Api.Models;
using Mapster;

namespace Classrom.Api.Mappers;

public static class ClassMapper
{
    public static ClassDto ToDto(this Class classes)
    {
        return new ClassDto
        {
            Id = classes.Id,
            Name = classes.Name,
            Key = classes.Key,
            Users = classes.UserClasses?.Select(u=>u.User?.Adapt<UserDto>()).Adapt<List<UserDto>>().ToList()
        };
    }
}
