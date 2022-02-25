using Finclusion.Database.Models;
using Finclusion.Database.Contexts;

namespace Finclusion.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly FinclusionContext _dbContext;
    public AccountService(FinclusionContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account?> AddFunds(string username, int amount)
    {
        var account = _dbContext.Accounts.FirstOrDefault(x => username.Equals(x.Username, StringComparison.InvariantCultureIgnoreCase));
        if(account == null){ return null; }
        account.Balance += amount;
        await _dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<Account?> SubtractFunds(string username, int amount)
    {
        var account = _dbContext.Accounts.FirstOrDefault(x => username.Equals(x.Username, StringComparison.InvariantCultureIgnoreCase));
        if(account == null){ return null; }
        account.Balance -= amount;
        await _dbContext.SaveChangesAsync();
        return account;
    }
}