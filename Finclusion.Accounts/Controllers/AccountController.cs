using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Finclusion.Accounts.Services;
using Finclusion.Database.Models;
using System.Security.Claims;

namespace Finclusion.Accounts.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [Route("AddFunds/amount={amount}")]
    public async Task<ActionResult<Account>> AddFunds(int amount)
    {
        var identity = this.User.Identity as ClaimsIdentity;
        var claimUsername = identity?.FindFirst(ClaimTypes.Name).Value;

        var result = await _accountService.AddFunds(claimUsername, amount);
        if(result == null)
        {
            return StatusCode(500, $"No matching user found for authentication token.");
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("SubtractFunds/amount={amount}")]
    public async Task<ActionResult<Account>> SubtractFunds(int amount)
    {
        var identity = this.User.Identity as ClaimsIdentity;
        var claimUsername = identity?.FindFirst(ClaimTypes.Name).Value;

        var result = await _accountService.SubtractFunds(claimUsername, amount);
        if(result == null)
        {
            return StatusCode(500, $"No matching user found for authentication token.");
        }
        return Ok(result);
    }
}
