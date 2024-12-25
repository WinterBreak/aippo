using Microsoft.AspNetCore.Identity;

namespace AdminPanel.Bll;

public interface AccountFactory
{ 
    Task<IdentityResult> CreateUserAsync(string username, string email, string password);
}