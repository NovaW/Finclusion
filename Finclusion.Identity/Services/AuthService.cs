using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Finclusion.Identity.Models;
using System.Security.Claims;

namespace Finclusion.Identity.Services;

public class AuthService : IAuthService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SigningCredentials _signingCredentials;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAccountService _accountService;

    public AuthService(UserManager<IdentityUser> userManager, IAccountService accountService)
    {
        var securityKey = Environment.GetEnvironmentVariable("SECURITY_KEY");
        var signingKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(securityKey));

        _userManager = userManager;
        _tokenHandler = new JwtSecurityTokenHandler();
        _signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        _accountService = accountService;
    }

    public async Task<AuthResult> Login(string username, string password)
    {
        var result = new AuthResult();

        var user = await _userManager.FindByNameAsync(username);
        if(user == null){
            result.Successful = false;
            result.Errors.Add(
                new IdentityError() { 
                    Code = "UserNotFound",
                    Description = $"No user found with username {username}."
                }
            );
            return result;    
        }

        var passwordMatches = await _userManager.CheckPasswordAsync(user, password);
        if(passwordMatches)
        {
            result.Successful = true;
            result.Token = await GenerateToken(username);
            return result;
        }
        else
        {
            result.Successful = false;
            result.Errors.Add(
                new IdentityError()
                {
                    Code = "InvalidPassword",
                    Description = $"Incorrect password for user {username}."
                }
            );
            return result;
        }
    }

    public async Task<AuthResult> Register(string username, string password)
    {
        var user = new IdentityUser { UserName = username };
        var result = await _userManager.CreateAsync(user, password);

        if(result.Succeeded)
        {
            await _accountService.CreateAccount(username);
        }

        return new AuthResult()
        {
            Successful = result.Succeeded,
            Errors = result.Errors.ToList(),
            Token = result.Succeeded ? await GenerateToken(username) : null
        };
    }
    private async Task<string> GenerateToken(string username)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                }),
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