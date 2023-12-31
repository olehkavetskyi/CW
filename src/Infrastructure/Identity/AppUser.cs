﻿using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppUser : IdentityUser<Guid>
{
    public Roles Role { get; set; }
}
