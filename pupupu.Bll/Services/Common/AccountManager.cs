using AdminPanel.Bll;
using Microsoft.AspNetCore.Identity;

namespace pupupu.Bll.Services;

public class AccountManager
{
    private AccountFactory _accountFactory;
    
    public AccountManager(AccountFactory accountFactory)
    {
        _accountFactory = accountFactory;
    }

    public async Task<IdentityResult> CreateAccountAsync(string username, string email, string password)
    {
        return await _accountFactory.CreateUserAsync(username, email, password);
    }
}