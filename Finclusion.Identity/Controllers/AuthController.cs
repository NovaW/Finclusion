using Microsoft.AspNetCore.Mvc;
using Finclusion.Database.Contexts;
using Finclusion.Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Finclusion.Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<string>> Get()
    {
        using(var dbContext = new FinclusionContext())
        {
            dbContext.Accounts.Add(new Account {
                Name = "test1"
            });
            await dbContext.SaveChangesAsync();
            var results = dbContext.Accounts.Select(x => x).ToList();
        }
        return Ok("hello world");
    }
}
