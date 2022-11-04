    using System;
using System.Security.Claims;
using AuthClaims.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace AuthClaims.Filters;
public class AuthAttribute : ActionFilterAttribute
{
    public readonly UserStore _store;
    public string Role { get; set; }

    public AuthAttribute(UserStore store, string roles)
    { _store = store; Role = roles; } 

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var key = context.HttpContext.Request.Headers[HeaderNames.Authorization];

        if(!_store.Users.ContainsKey(key))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var users = _store.Users[key];

        if (!Role.Contains(users.Role!))
        {
            context.Result = new JsonResult(new { Error = "Siz admin massiz" });
            return;
        }

        var claims = new List<Claim>()
        {
             new Claim (ClaimTypes.Name, users.Name!),
             new Claim (ClaimTypes.MobilePhone, users.Phone!),
             new Claim (ClaimTypes.Email, users.Email!),
             new Claim (ClaimTypes.Role, users.Role!)
        };
        var identity = new ClaimsIdentity(claims);
        var principal = new ClaimsPrincipal(identity);
        context.HttpContext.User = principal;
    }

}