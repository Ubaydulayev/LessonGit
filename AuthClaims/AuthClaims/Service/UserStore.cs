using System;
using AuthClaims.Models;

namespace AuthClaims.Service;

public class UserStore
{
    public Dictionary<string, User> Users;

    public UserStore()
    {
        Users = new Dictionary<string, User>();
    }
}


