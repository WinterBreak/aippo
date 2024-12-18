using System.ComponentModel.DataAnnotations;

namespace pupupu.Web.ViewModels.User;

public class UserViewModel
{
    public UserViewModel() { }

    public UserViewModel(string userId, string email, string username)
    {
        this.Id = userId;
        this.Email = email;
        this.UserName = username;
    }
    
    public string Id { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string UserName { get; set; }
}