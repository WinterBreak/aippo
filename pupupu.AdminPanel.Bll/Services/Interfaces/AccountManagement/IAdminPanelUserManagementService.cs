
using AdminPanel.Dal;
using pupupu.Bll.Dto;


namespace AdminPanel.Bll;

public interface IAdminPanelUserManagementService
{
    List<User> GetUsers(UserListQuery query);
    
    User GetUserById(string id);
    
    void EditUser(UserDto query);
    
    void DeleteUser(string userId);
    
    
}