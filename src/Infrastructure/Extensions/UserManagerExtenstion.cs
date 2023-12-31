﻿using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Infrastructure.Extensions;

public static class UserManagerExtensions
{
    public static async Task<AppUser?> FindByEmailFromClaimsPrincipalAsync(this UserManager<AppUser> userManager,
    ClaimsPrincipal user)
    {
        return await userManager.Users
            .SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
    }
}