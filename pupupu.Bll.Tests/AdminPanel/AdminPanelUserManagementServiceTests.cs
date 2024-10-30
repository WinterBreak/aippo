using Moq;
using pupupu.Models.DAL;
using pupupu.Repositories.Interfaces;
using pupupu.Services;
using pupupu.Services.Interfaces;
using pupupu.ViewModels.User;

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
    
    // TODO доп тесты для GetUserById, когда будет реализована сортировка по типу пользователя
    
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
        var userVm = new UserViewModel(NON_EXISTING_USER_ID, "email", "password");
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(userVm.Id))
            .Returns((User)null);
        
        Assert.Throws<ArgumentNullException>(() => _service.EditUser(userVm));
    }

    [Test]
    public void EditUser_ExistingUser_NoException()
    {
        var userVm = new UserViewModel(CORRECT_USER_ID, "email", "password");
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
        var userVm = new UserViewModel(NON_EXISTING_USER_ID, "email", "password");
        
        _mockUserRepository
            .Setup(repo => repo.GetUserById(userVm.Id))
            .Returns((User)null);
        
        Assert.Throws<ArgumentNullException>(() => _service.DeleteUser(userVm.Id));
    }

    [Test]
    public void DeleteUser_ExistingUser_NoException()
    {
        var userVm = new UserViewModel(CORRECT_USER_ID, "email", "password");
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