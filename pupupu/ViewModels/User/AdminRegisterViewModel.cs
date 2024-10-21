using System.ComponentModel.DataAnnotations;

namespace pupupu.ViewModels.User;

public class AdminRegisterViewModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } 
    
    // TODO сюда мб юзер тайп??
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
    public string ConfirmPassword { get; set; }
}