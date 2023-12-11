using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppRole : IdentityRole<Guid>
{
    public Roles Role { get; set; }
}
