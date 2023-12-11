using Application.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Identity;
using Infrastructure.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Web.Dtos;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountService(ITokenService tokenService, 
        UserManager<AppUser> userManager, 
        SignInManager<AppUser> signInManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<UserDto> GetCurrentUser(ClaimsPrincipal userClaims)
    {
        var user = await _userManager.FindByEmailFromClaimsPrincipalAsync(userClaims);

        return new UserDto
        {
            Email = user?.Email!,
            Token = _tokenService.GenerateJwtToken(user!)
        };
    }

    public async Task<UserDto?> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return null;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (result.Succeeded)
        {
            return new UserDto
            {
                Email = user.Email!,
                Token = _tokenService.GenerateJwtToken(user),
            };
        }

        return null;
    }
}
