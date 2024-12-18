using Microsoft.AspNetCore.Identity;

namespace pupupu.Dal.Models;

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