using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pupupu.Models.DAL;
using pupupu.Services.Interfaces;
using pupupu.ViewModels.User;

namespace pupupu.Controllers;

[Route("AdminPanel")]
public class AdminPanelController: Controller // отдельный контроллер, тк по идее админка должна стоять отдельно
    // отдельно собираться и запускаться. в будущем - отдельный сервис, вероятно
    // сразу отделим от основного хотя бы на таком уровне
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAdminPanelUserManagementService _service;

    public AdminPanelController(UserManager<User> userManager
        , SignInManager<User> signInManager
        , IAdminPanelUserManagementService service)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _service = service;
    }
    
    [HttpPost("RegisterSubmit")]
    public async Task<IActionResult> RegisterSubmit(AdminRegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                Name = model.Name,
                UserName = model.Email,
                Email = model.Email,
                UserType = (int)UserType.Admin
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var users = _service.GetUsers();
                var userList = new UserList(users);
                return RedirectToAction("UserList", userList);
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

    [HttpGet("")]
    public IActionResult UserList() // TODO фильтры
    {
        var users = _service.GetUsers();
        var userList = new UserList(users); // по приколу можно вынести в билдер
        return View(userList);
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
    public IActionResult EditUserSubmit(UserViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(); // TODO надо какое-то модальное окно для ошибок намутить
        }
        _service.EditUser(user);
        var users = _service.GetUsers();
        var userList = new UserList(users);
        return RedirectToAction("UserList", userList); 
    }
    
    [HttpPost("DeleteUser/{id}")]
    public IActionResult DeleteUser(string id)
    {
        _service.DeleteUser(id);
        var users = _service.GetUsers();
        var userList = new UserList(users);
        return View("UserList", userList);
    }

}