using Finclusion.Identity.Models;

namespace Finclusion.Identity.Services;

public interface IAuthService
{
    Task<string?> Login (string username, string password);
    Task<RegistrationResult> Register (string username, string password);
}