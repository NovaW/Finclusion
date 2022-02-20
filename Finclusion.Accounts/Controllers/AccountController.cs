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
    [Route("AddFunds/username={username}&amount={amount}")]
    public async Task<ActionResult<Account>> AddFunds(string username, int amount)
    {
        if(await VerifyUser(username))
        {
            var account = await _accountService.GetAccountByUserName(username);
            if(account == null)
            {
                return NotFound();
            }

            var result = await _accountService.AddFunds(account.Id, amount);
            return Ok(result);
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpPost]
    [Route("SubtractFunds/username={username}&amount={amount}")]
    public async Task<ActionResult<Account>> SubtractFunds(string username, int amount)
    {
        if(await VerifyUser(username))
        {
            var account = await _accountService.GetAccountByUserName(username);
            if(account == null)
            {
                return NotFound();
            }

            var result = await _accountService.SubtractFunds(account.Id, amount);
            return Ok(result);
        }
        else
        {
            return Unauthorized();
        }
    }

    private async Task<bool> VerifyUser(string username)
    {
        //I'm sure there's a cleaner way of doing this
        var identity = this.User.Identity as ClaimsIdentity;
        var claimUsername = identity?.FindFirst(ClaimTypes.Name).Value;
        return claimUsername != null && claimUsername.Equals(username, StringComparison.InvariantCultureIgnoreCase);
    }
}
