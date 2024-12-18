using Microsoft.AspNetCore.Identity;

namespace pupupu.Bll.Services;

public interface AccountFactory
{ 
    Task<IdentityResult> CreateUserAsync(string username, string email, string password);
}