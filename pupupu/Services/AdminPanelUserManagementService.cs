using pupupu.Models.DAL; // здесь дал потому что для юзера нет смысла в блл модели
using pupupu.Repositories.Interfaces;
using pupupu.Services.Interfaces;
using System.Linq;
using pupupu.Queries;
using pupupu.ViewModels.User;

namespace pupupu.Services;

public class AdminPanelUserManagementService: IAdminPanelUserManagementService
{
    private readonly IUserRepository _userRepository;

    public AdminPanelUserManagementService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<User> GetUsers(UserListQuery query) // TODO сюда пробросить фильтры
    {
        var users = _userRepository
            .GetAllUsers().OrderBy(u => u.Name).ToList();
        GetFilteredUsersByUserType(users, query.UserType);
        return users;
    }

    public User GetUserById(string userId)
    {
        return _userRepository.GetUserById(userId);
    }

    public void EditUser(UserViewModel query)
    {
        var user = _userRepository.GetUserById(query.Id);
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        user.Email = query.Email;
        user.Name = query.UserName;
        _userRepository.SaveChanges();
    }

    public void DeleteUser(string userId)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        
        _userRepository.RemoveUser(user);
        _userRepository.SaveChanges();
    }
    
    private void GetFilteredUsersByUserType(List<User> users, UserType userType)
    {
        if (!Enum.IsDefined(typeof(UserType), userType))
        {
            throw new ArgumentException("Неверный тип пользователя!");
        }

        if (userType != UserType.None)
        {
            users.RemoveAll(u => u.UserType != (int)userType);
        }
    }
}