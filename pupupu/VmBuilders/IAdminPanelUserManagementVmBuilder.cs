using pupupu.Queries;
using pupupu.ViewModels.User;

namespace pupupu.VmBuilders;

public interface IAdminPanelUserManagementVmBuilder
{
    UserList GetUserListVm(UserListQuery query);
}