using Microsoft.AspNetCore.Identity;

namespace Finclusion.Identity.Models;

public class RegistrationResult
{
    public bool Sucessful { get; set; }
    public IEnumerable<IdentityError>? Errors { get; set; }
    public string? Token { get; set; }
}