using Microsoft.AspNetCore.Identity;

namespace pupupu.Models.DAL;

public class User: IdentityUser
{
    public string Name { get; set; }

    public int UserType { get; set; }
}