using System;
namespace Mapper.Models;
public static class Converter
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto()
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };
    }
    public static QuestionDto ToQuestionDto(this Question question)
    {
        return new QuestionDto()
        {
            Title = question.Title,
            Choice = question.Choice
        };
    }
}