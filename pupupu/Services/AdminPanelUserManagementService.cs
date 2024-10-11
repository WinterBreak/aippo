using pupupu.Repositories.Interfaces;
using pupupu.Services.Interfaces;

namespace pupupu.Services;

public class AdminPanelUserManagementService: IAdminPanelUserManagementService
{
    private readonly IUserRepository _userRepository;

    public AdminPanelUserManagementService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
}