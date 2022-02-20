using Microsoft.AspNetCore.Identity;

namespace Finclusion.Identity.Models;

public class AuthResult
{
    public AuthResult()
    {
        Errors = new List<IdentityError>();
    }
    public bool Successful { get; set; }
    public IList<IdentityError> Errors { get; set; }
    public string? Token { get; set; }
}