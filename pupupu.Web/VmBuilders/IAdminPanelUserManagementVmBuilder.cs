using pupupu.Bll.Dto;
using pupupu.Web.ViewModels.User;

namespace pupupu.Web.VmBuilders;

public interface IAdminPanelUserManagementVmBuilder
{
    UserList GetUserListVm(UserListQuery query);
}