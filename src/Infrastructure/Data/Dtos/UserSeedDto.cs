using Infrastructure.Identity;

namespace Infrastructure.Data.Dtos;

public class UserSeedDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Roles Role { get; set; }
}