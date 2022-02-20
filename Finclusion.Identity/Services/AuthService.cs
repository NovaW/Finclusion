using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Finclusion.Identity.Models;

namespace Finclusion.Identity.Services;

public class AuthService : IAuthService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SigningCredentials _signingCredentials;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthService(UserManager<IdentityUser> userManager)
    {
        var securityKey = Environment.GetEnvironmentVariable("SECURITY_KEY");
        var signingKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(securityKey));

        _userManager = userManager;
        _tokenHandler = new JwtSecurityTokenHandler();
        _signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
    }

    public async Task<string?> Login(string username, string password)
    {
        var user = new IdentityUser { UserName = username };
        var passwordMatches = await _userManager.CheckPasswordAsync(user, password);
        if(passwordMatches)
        {
            return await GenerateToken();
        }
        return null;
    }

    public async Task<RegistrationResult> Register(string username, string password)
    {
        var user = new IdentityUser { UserName = username };
        var result = await _userManager.CreateAsync(user, password);

        return new RegistrationResult()
        {
            Sucessful = result.Succeeded,
            Errors = result.Errors,
            Token = result.Succeeded ? await GenerateToken() : null
        };
    }
    private async Task<string> GenerateToken()
    {
        var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = _signingCredentials,
                Issuer = "finclusion",
                Audience = "finclusion",
                NotBefore = DateTime.UtcNow,
            };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }
}