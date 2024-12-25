using pupupu.Bll.Dto;

namespace AdminPanel.Web;

public interface IAdminPanelUserManagementVmBuilder
{
    UserList GetUserListVm(UserListQuery query);
}