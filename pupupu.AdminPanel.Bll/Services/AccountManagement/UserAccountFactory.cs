using AdminPanel.Bll;
using AdminPanel.Dal;
using Microsoft.AspNetCore.Identity;
using pupupu.Dal.Models;

namespace AdminPanel.Bll;

public class UserAccountFactory: AccountFactory
{
    private readonly UserManager<User> _userManager;

    public UserAccountFactory(UserManager<User> userManager)
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
            UserType = (int)UserType.User
        };

        return await _userManager.CreateAsync(user, password);
    }
}