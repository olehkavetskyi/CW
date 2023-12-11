namespace Infrastructure.Identity.Interface;

public interface ITokenService
{
    string GenerateJwtToken(AppUser appUser);
    string GetEmailFromJwtToken(string jwtToken);
    Task<bool> CheckEmailExistsAsync(string email);
}
