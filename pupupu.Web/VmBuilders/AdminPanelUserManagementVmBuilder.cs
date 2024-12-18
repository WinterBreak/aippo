using pupupu.Bll.Dto;
using pupupu.Bll.Services;
using pupupu.Web.ViewModels.User;

namespace pupupu.Web.VmBuilders;

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