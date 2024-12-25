using AdminPanel.Bll;
using AdminPanel.Dal;
using Moq;
using pupupu.Bll.Dto;
namespace pupupu.Bll.Tests;

[TestFixture]
public class AdminPanelUserManagementServiceTests
{
    const string NON_EXISTING_USER_ID = "incorrect_user_id";
    const string CORRECT_USER_ID = "correct_user_id";
    
    private Mock<IAdminPanelUserManagementService> _mockAdminPanelService;
    private Mock<IUserRepository> _mockUserRepository;
    private IAdminPanelUserManagementService _service;
    
    [SetUp]
    public void SetUp()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockAdminPanelService = new Mock<IAdminPanelUserManagementService>();
        _service = new AdminPanelUserManagementService(_mockUserRepository.Object);
        _mockAdminPanelService
            .Setup(service => service.GetUserById(It.IsAny<string>()))
            .Returns((string userId) => _service.GetUserById(userId));
    }

    [Test]
    public void GetUsers_ShouldReturnAllUsers()
    {
        var query = new UserListQuery();
        var userList = new List<User>
        {
            new User {UserName = "Admin", UserType = (int)UserType.Admin}, // TODO сделать тест кейс?
            new User {UserName = "User", UserType = (int)UserType.User},
        };
        
        _mockUserRepository
            .Setup(repo => repo.GetAllUsers())
            .Returns(userList.AsQueryable());
        
        var result = _service.GetUsers(query);
        Assert.That(result, Is.EquivalentTo(userList));
    }

    [Test]
    public void GetUsers_ShouldReturnAdminUsers()
    {
        var query = new UserListQuery { UserType = UserType.Admin };
        var userList = new List<User>
        {
            new User {UserName = "Admin", UserType = (int)UserType.Admin},
            new User {UserName = "User", UserType = (int)UserType.User},
        };
        var expectedUserList = userList
            .Where(user => user.UserType == (int)UserType.Admin).ToList();
        
        _mockUserRepository
            .Setup(repo => repo.GetAllUsers())
            .Returns(userList.AsQueryable());
        
        var result = _service.GetUsers(query);
        Assert.That(result, Is.EquivalentTo(expectedUserList));
    }

    [Test]
    public void GetUsers_ShouldReturnUsers()
    {
        var query = new UserListQuery { UserType = UserType.User };
        var userList = new List<User>
        {
            new User {UserName = "Admin", UserType = (int)UserType.Admin},
            new User {UserName = "User", UserType = (int)UserType.User},
        };
        var expectedUserList = userList
            .Where(user => user.UserType == (int)UserType.User).ToList();
        
        _mockUserRepository
            .Setup(repo => repo.GetAllUsers())
            .Returns(userList.AsQueryable());
        
        var result = _service.GetUsers(query);
        Assert.That(result, Is.EquivalentTo(expectedUserList));
    }

    [Test]
    public void GetUsers_ShouldReturnEmptyListIfNoUsers()
    {
        var query = new UserListQuery { UserType = UserType.User };
        var userList = new List<User>
        {
            new User {UserName = "Admin", UserType = (int)UserType.Admin},
            new User {UserName = "User", UserType = (int)UserType.Admin},
        };
        
        _mockUserRepository
            .Setup(repo => repo.GetAllUsers())
            .Returns(userList.AsQueryable());
        
        var result = _service.GetUsers(query);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void GetUsers_ShouldThrowExceptionIfNonExistingUserType()
    {
        const int WRONG_USER_TYPE = -999;
        var query = new UserListQuery { UserType =  (UserType)WRONG_USER_TYPE };
        var userList = new List<User>
        {
            new User {UserName = "Admin", UserType = (int)UserType.Admin},
            new User {UserName = "User", UserType = (int)UserType.Admin},
        };
        _mockUserRepository
            .Setup(repo => repo.GetAllUsers())
            .Returns(userList.AsQueryable());
        
        Assert.Throws<ArgumentException>(() => _service.GetUsers(query));
    }
    
    [Test]
    public void GetUserById_CorrectId_ReturnsUser()
    {
        var expectedUser = new User
        {
            Id = CORRECT_USER_ID,
            Name = "Пользователь"
        };
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(CORRECT_USER_ID))
            .Returns(expectedUser);
        
        var result = _mockAdminPanelService.Object.GetUserById(CORRECT_USER_ID);
        
        Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
        Assert.That(result.Name, Is.EqualTo(expectedUser.Name));
    }

    [Test]
    public void GetUserById_NonExistingId_ReturnsNull()
    {
        _mockUserRepository
            .Setup(repo => repo.GetUserById(NON_EXISTING_USER_ID))
            .Returns((User)null);

        var result = _mockAdminPanelService.Object.GetUserById(NON_EXISTING_USER_ID);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void EditUser_NonExistingUser_ThrowsException()
    {
        var userVm = new UserDto(NON_EXISTING_USER_ID, "email", "password");
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(userVm.Id))
            .Returns((User)null);
        
        Assert.Throws<ArgumentNullException>(() => _service.EditUser(userVm));
    }

    [Test]
    public void EditUser_ExistingUser_NoException()
    {
        var userVm = new UserDto(CORRECT_USER_ID, "email", "password");
        var expectedUserFromRepo = new User
        {
            Id = CORRECT_USER_ID,
            Email = userVm.Email,
        };
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(userVm.Id))
            .Returns(expectedUserFromRepo);
        
        Assert.DoesNotThrow(() => _service.EditUser(userVm));
    }

    [Test]
    public void DeleteUser_NonExistingUser_ThrowsException()
    {
        var userVm = new UserDto(NON_EXISTING_USER_ID, "email", "password");
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(userVm.Id))
            .Returns((User)null);
        
        Assert.Throws<ArgumentNullException>(() => _service.DeleteUser(userVm.Id));
    }

    [Test]
    public void DeleteUser_ExistingUser_NoException()
    {
        var userVm = new UserDto(CORRECT_USER_ID, "email", "password");
        var expectedUser = new User
        {
            Id = CORRECT_USER_ID,
            Email = userVm.Email,
        };
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(userVm.Id))
            .Returns(expectedUser);
        
        Assert.DoesNotThrow(() => _service.DeleteUser(userVm.Id));
    }
}