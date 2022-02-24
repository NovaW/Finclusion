using Microsoft.AspNetCore.Mvc;
using Finclusion.Identity.Services;
using Finclusion.Identity.Models;

namespace Finclusion.Identity.Controllers;


[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("login/username={username}&password={password}")]
    public async Task<ActionResult<AuthResult>> Login(string username, string password)
    {
        var result = await _authService.Login(username, password);
        if(result.Successful)
        {
            return Ok(result);
        }
        return StatusCode(500, result);
    }

    [HttpPost]
    [Route("register/username={username}&password={password}")]
    public async Task<ActionResult<AuthResult>> Register(string username, string password)
    {
        var result = await _authService.Register(username, password);

        if(result.Successful)
        {
            return Ok(result);
        }
        return StatusCode(500, result);
    }
}