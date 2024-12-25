using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Web;

public class RegisterViewModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
    public string ConfirmPassword { get; set; }
}