using System.Security.Claims;
using Web.Dtos;

namespace Application.Interfaces;

public interface IAccountService
{
    Task<bool> CheckEmailExistsAsync(string email);
    Task<UserDto?> LoginAsync(LoginDto loginDto);
    Task<UserDto> GetCurrentUser(ClaimsPrincipal userClaims);
}
