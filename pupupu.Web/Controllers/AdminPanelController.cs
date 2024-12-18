using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pupupu.Dal.Models;
using pupupu.Bll.Services;
using pupupu.Bll.Dto;
using pupupu.Web.ViewModels.User;
using pupupu.Web.VmBuilders;

namespace pupupu.Web.Controllers;

[Route("AdminPanel")]
public class AdminPanelController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAdminPanelUserManagementService _service;
    private readonly IAdminPanelUserManagementVmBuilder _adminPanelUserManagementVmBuilder;

    public AdminPanelController(UserManager<User> userManager
        , SignInManager<User> signInManager
        , IAdminPanelUserManagementService service
        , IAdminPanelUserManagementVmBuilder adminPanelUserManagementVmBuilder)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _service = service;
        _adminPanelUserManagementVmBuilder = adminPanelUserManagementVmBuilder;
    }

    public IActionResult Index()
    {
        var userList = _adminPanelUserManagementVmBuilder.GetUserListVm(new UserListQuery());
        return View(userList);
    }
    
    [HttpPost("RegisterSubmit")]
    public async Task<IActionResult> RegisterSubmit(AdminRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userFactory = new AdminAccountFactory(_userManager);
            var manager = new AccountManager(userFactory);
            var result = await manager.CreateAccountAsync(model.Name, model.Email, model.Password);
            
            if (result.Succeeded)
            {
                var userList = _adminPanelUserManagementVmBuilder.GetUserListVm(new UserListQuery());
                return RedirectToAction("Index", userList);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        return BadRequest(ModelState);
    }
    
    [HttpGet("Register")]
    public IActionResult Register()
    {
        return PartialView("CreateUser", new AdminRegisterViewModel());
    }
    
    [HttpPost("Login")]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email
                , model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Url.IsLocalUrl(returnUrl) 
                    ? Redirect(returnUrl)
                    : RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }
        
        
        return View(model);
    }
    
    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet("UserList")]
    public IActionResult UserList(UserListQuery query)
    {
        var userList = _adminPanelUserManagementVmBuilder.GetUserListVm(query);
        return PartialView(userList);
    }
    
    [HttpGet("EditUser/{id}")]
    public ActionResult EditUser(string id)
    {
        var user = _service.GetUserById(id);
        if (user is null)
        {
            return NotFound();
        }
        var userVm = new UserViewModel(id, user.Email, user.Name);
        return PartialView(userVm);
    }
    
    [HttpPost("EditUserSubmit")]
    public IActionResult EditUserSubmit(UserDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(); // TODO надо какое-то модальное окно для ошибок намутить
        }
        _service.EditUser(user);
        
        var userList = _adminPanelUserManagementVmBuilder.GetUserListVm(new UserListQuery());
        return RedirectToAction("UserList", userList); 
    }
    
    [HttpPost("DeleteUser/{id}")]
    public IActionResult DeleteUser(string id)
    {
        _service.DeleteUser(id);
        var userList =_adminPanelUserManagementVmBuilder.GetUserListVm(new UserListQuery());
        return View("UserList", userList);
    }

}