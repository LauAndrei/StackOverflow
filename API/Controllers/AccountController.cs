using API.Constants;
using API.Dtos;
using Core.Constants;
using Core.Entities;
using Core.EntityExtensions.UserExtensions;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        User user;
        if (!string.IsNullOrWhiteSpace(loginDto.Email))
        {
            user = await _userManager.FindByEmailAsync(loginDto.Email);
        }
        else if (!string.IsNullOrWhiteSpace(loginDto.UserName))
        {
            user = await _userManager.FindByNameAsync(loginDto.UserName);
        }
        else
        {
            throw new ArgumentNullException(
                $"{nameof(loginDto.Email)} or {nameof(loginDto.UserName)} cannot be empty!"
                );
        }

        if (user == null)
        {
            return Unauthorized("Unauthorized!");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(RESPONSE_CONSTANTS.USER.INCORRECT_CREDENTIALS);
        }
        
        
        return user.ToUserDto(await _tokenService.CreateToken(user));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto register)
    {
        var user = register.ToUser();

        var result = await _userManager.CreateAsync(user, register.Password);

        //TODO: Create some better exceptions
        if (!result.Succeeded)
        {
            return BadRequest("Error creating an account");
        }

        var result2 = await _userManager.AddToRoleAsync(user, ROLES_CONSTANTS.ROLES.STANDARD);
        
        if (!result2.Succeeded)
        {
            return BadRequest("Error assigning role");
        }
        
        return user.ToUserDto(await _tokenService.CreateToken(user));
    }
}