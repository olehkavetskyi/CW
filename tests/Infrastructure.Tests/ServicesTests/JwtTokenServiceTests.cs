using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.Text;

namespace Infrastructure.Tests.ServicesTests;

public class TokenServiceTests
{
    [Fact]
    public void GenerateJwtToken_ShouldGenerateToken()
    {
        // Arrange
        var config = new Mock<IConfiguration>();
        config.Setup(c => c["Token:Key"]).Returns("4A781BF9B41F6C46B0FE99F404C44E1520A04A8B15517A73A357F82D5A71A2C8");
        config.Setup(c => c["Token:Issuer"]).Returns("issuer");
        config.Setup(c => c["Token:Audience"]).Returns("audience");
        config.Setup(c => c["Token:JwtTokenValidityInMinutes"]).Returns("30");

        var userManager =new Mock<UserManager<AppUser>>(
            Mock.Of<IUserStore<AppUser>>(),
            null, null, null, null, null, null, null, null
        );

        var tokenService = new TokenService(config.Object, null, userManager.Object);
        var user = new AppUser { UserName = "testuser", Role = Roles.Manager };

        // Act
        var result = tokenService.GenerateJwtToken(user);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }
}