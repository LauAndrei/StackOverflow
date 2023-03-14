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
    public async Task<ActionResult<LoggedInUserDto>> Login(LoginDto loginDto)
    {
        User user;
        if (loginDto.UserNameOrEmail.Contains('@'))
        {
            user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
        }
        else
        {
            user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
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
        
        return user.ToLoggedInUserDto(await _tokenService.CreateToken(user));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<LoggedInUserDto>> Register(RegisterDto register)
    {
        var findUser = await _userManager.FindByEmailAsync(register.Email);
        if (findUser is not null)
        {
            return BadRequest("Error: Email already in use");
        }

        findUser = await _userManager.FindByNameAsync(register.UserName);
        if (findUser is not null)
        {
            return BadRequest("Error: Username already in use");
        }
        
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
        
        return user.ToLoggedInUserDto(await _tokenService.CreateToken(user));
    }
}