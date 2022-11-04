using System;
using Microsoft.AspNetCore.Mvc;

namespace AuthClaims.Filters;
public class RoleAttribute : TypeFilterAttribute
{
    public RoleAttribute(string role) : base(typeof(AuthAttribute))
    {
        Arguments = new object[] { role };
    }
}
