using System;
using Ui2.Models;

namespace Ui2.Service;
public class UserStore
{
    public Dictionary<string, User> Users;

    public UserStore()
    {
        Users = new Dictionary<string, User>();
    }
}


