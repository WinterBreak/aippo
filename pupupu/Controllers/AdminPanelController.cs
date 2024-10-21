using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pupupu.Models.DAL;
using pupupu.Services.Interfaces;
using pupupu.ViewModels.User;

namespace pupupu.Controllers;

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
    
    public async Task<IActionResult> Register(AdminRegisterViewModel model)
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
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        return View(model);
    }
    
    [HttpPost]
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
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult UserList() // TODO фильтры
    {
        var users = _service.GetUsers();
        var userList = new UserList(users);
        return View(userList);
    }

    [HttpPost]
    public IActionResult EditUser(User user)
    {
        _service.EditUser(user);
        var users = _service.GetUsers();
        return View(users);
    }

    [HttpDelete]
    public IActionResult DeleteUser(User user)
    {
        _service.DeleteUser(user);
        var users = _service.GetUsers();
        return View(users);
    }

}