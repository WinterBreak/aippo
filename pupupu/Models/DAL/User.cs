using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using pupupu.Services;

namespace pupupu.Models.DAL;

public class User: IdentityUser
{
    public string Name { get; set; }

    public int UserType { get; set; }

    public User(){}
    public User(string name, string email, int userType)
    {
        this.Name = name;
        this.Email = email;
        this.UserName = email;
        this.UserType = userType;
    }
}