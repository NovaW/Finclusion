using Finclusion.Identity.Models;

namespace Finclusion.Identity.Services;

public interface IAuthService
{
    Task<AuthResult> Login (string username, string password);
    Task<AuthResult> Register (string username, string password);
}