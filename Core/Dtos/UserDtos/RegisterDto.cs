using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.UserDtos;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    //[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}