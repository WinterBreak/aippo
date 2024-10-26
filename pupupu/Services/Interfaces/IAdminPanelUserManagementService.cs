using pupupu.Models.DAL;
using pupupu.ViewModels.User;

namespace pupupu.Services.Interfaces;

public interface IAdminPanelUserManagementService
{
    List<User> GetUsers();
    
    User GetUserById(string id);
    
    void EditUser(UserViewModel query);
    
    void DeleteUser(string userId);
}