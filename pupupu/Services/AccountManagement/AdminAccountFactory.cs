using Microsoft.AspNetCore.Identity;
using pupupu.Models.DAL;

namespace pupupu.Services;

public class AdminAccountFactory: AccountFactory
{
    private readonly UserManager<User> _userManager;

    public AdminAccountFactory(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IdentityResult> CreateUserAsync(string username, string email, string password)
    {
        var user = new User
        {
            Name = username,
            UserName = email,
            Email = email,
            UserType = (int)UserType.Admin
        };

        return await _userManager.CreateAsync(user, password);
    }
}