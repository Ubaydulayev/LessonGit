using System;
using AuthClaims.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
namespace AuthClaims.Handler;

public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public AuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) :   
        base(options, logger, encoder, clock)
    { }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>()
        {
            new Claim (ClaimTypes.Name, "users.Name!"),
            new Claim (ClaimTypes.MobilePhone, "users.Phone!"),
             new Claim (ClaimTypes.Email, "users.Email!")
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return await Task.FromResult(AuthenticateResult.Success(ticket));
    }
}


