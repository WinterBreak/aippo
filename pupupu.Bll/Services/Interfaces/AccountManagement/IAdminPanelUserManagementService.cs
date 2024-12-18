using pupupu.Dal.Models;
using pupupu.Bll.Dto;

namespace pupupu.Bll.Services;

public interface IAdminPanelUserManagementService
{
    List<User> GetUsers(UserListQuery query);
    
    User GetUserById(string id);
    
    void EditUser(UserDto query);
    
    void DeleteUser(string userId);
    
    
}