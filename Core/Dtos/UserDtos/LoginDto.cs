using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.UserDtos;

public class LoginDto
{
    [Required]
    public string UserNameOrEmail { get; set; }
    
    [Required]
    public string Password { get; set; }
}