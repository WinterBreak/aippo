using pupupu.Models.DAL;
using pupupu.Queries;
using pupupu.ViewModels.User;

namespace pupupu.Services.Interfaces;

public interface IAdminPanelUserManagementService
{
    List<User> GetUsers(UserListQuery query);
    
    User GetUserById(string id);
    
    void EditUser(UserViewModel query);
    
    void DeleteUser(string userId);
    
    
}