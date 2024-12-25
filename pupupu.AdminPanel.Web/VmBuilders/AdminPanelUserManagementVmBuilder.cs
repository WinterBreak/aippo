using AdminPanel.Bll;
using pupupu.Bll.Dto;

namespace AdminPanel.Web;

public class AdminPanelUserManagementVmBuilder: IAdminPanelUserManagementVmBuilder
{
    private readonly IAdminPanelUserManagementService _service;

    public AdminPanelUserManagementVmBuilder(IAdminPanelUserManagementService service)
    {
        _service = service;
    }
    
    public UserList GetUserListVm(UserListQuery query)
    {
        var users = _service.GetUsers(query);
        return new UserList(users);
    }
}