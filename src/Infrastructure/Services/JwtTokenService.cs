using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Identity.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenService : ITokenService
{
    public readonly IConfiguration _config;
    public readonly SymmetricSecurityKey _key;
    public readonly CwContext _context;
    public readonly UserManager<AppUser> _userManager;

    public TokenService(IConfiguration config, CwContext context, UserManager<AppUser> userManager)
    {
        _config = config;
        _context = context;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]!));
        _userManager = userManager;
    }

    public string GenerateJwtToken(AppUser appUser)
    {
        var claims = new[]
        {
            new Claim("email", appUser.UserName!),
            new Claim(ClaimTypes.Role, appUser.Role.ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _config["Token:Issuer"],
            audience: _config["Token:Audience"],
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Token:JwtTokenValidityInMinutes"])),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GetEmailFromJwtToken(string jwtToken)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);

        return jwt.Claims.First(c => c.Type == "email").Value;
    }

    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }
}
