using System;
using UserAutentication.Models;
using UserAutentication.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UserAutentication.Filters
{
    public class AuthFilterAttribute : ActionFilterAttribute
    {
        protected readonly UsersStore _usersStore;

        public AuthFilterAttribute(UsersStore usersStore)
        {
            _usersStore = usersStore;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Key"))
                context.Result = new UnauthorizedResult();
            return;

            var key = context.HttpContext.Request.Headers["Key"];

            if (!_usersStore.Users.ContainsKey(key))
            {
                context.Result = new UnauthorizedResult();
                //return;
            }
        }
    }
}

