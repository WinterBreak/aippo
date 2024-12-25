using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Web;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Display(Name = "Запомнить меня?")]
    public bool RememberMe { get; set; }
}