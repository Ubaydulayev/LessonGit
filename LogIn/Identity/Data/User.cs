using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data;
public class User : IdentityUser
{
    public long ChatId { get; set; }
    public List<UserAddress> Addresses { get; set; }
}
