﻿using System.Security.Claims;
using Core.Constants;
using Core.Dtos.UserDtos;
using Core.Entities;
using Core.EntityExtensions.UserExtensions;
using Core.Exceptions;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


    [HttpGet]
    [Authorize]
    public async Task<ActionResult<LoggedInUserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(email);

        return new LoggedInUserDto
        {
            Email = user.Email,
            UserName = user.UserName,
            Score = 0,
            Token = await _tokenService.CreateToken(user)
        };
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
            throw new EmailAlreadyInUseException();
        }

        findUser = await _userManager.FindByNameAsync(register.UserName);
        if (findUser is not null)
        {
            throw new UserNameAlreadyInUseException();
        }
        
        var user = register.ToUser();

        var result = await _userManager.CreateAsync(user, register.Password);
        
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

    [HttpGet]
    [Route("GetAllUsers")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MODERATOR)]
    public async Task<List<UserDto>> GetAllUsers()
    {
       return await _userManager.Users.Select(u => u.ToUserDto()).ToListAsync();
    }
}