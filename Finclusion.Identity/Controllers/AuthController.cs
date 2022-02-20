using Microsoft.AspNetCore.Mvc;
using Finclusion.Identity.Services;

namespace Finclusion.Identity.Controllers;


[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    [Route("login/username={username}&password={password}")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var token = await _authService.Login(username, password);
        if(token == null)
        {
            return NotFound();
        }
        return Ok(token);
    }

    [HttpPost]
    [Route("register/username={username}&password={password}")]
    public async Task<IActionResult> Register(string username, string password)
    {
        var result = await _authService.Register(username, password);

        if(result.Sucessful)
        {
            return Ok(result);
        }
        return StatusCode(500, result);
    }
}