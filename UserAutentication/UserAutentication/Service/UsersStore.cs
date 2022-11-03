using System;
using UserAutentication.Models;
namespace UserAutentication.Service;
public class UsersStore
{
    public Dictionary<string, User> Users;
    public UsersStore()
    {
        Users = new Dictionary<string, User>();
    }
}


