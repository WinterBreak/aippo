using pupupu.Models.DAL;

namespace pupupu.Services.Interfaces;

public interface IAdminPanelUserManagementService
{
    List<User> GetUsers();
    
    User EditUser(User user);
    
    void DeleteUser(User user);
}