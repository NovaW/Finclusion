using Finclusion.Database.Models;
using Finclusion.Database.Contexts;

namespace Finclusion.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly FinclusionContext _dbContext;
    public AccountService(FinclusionContext dbContext
    )
    {
        _dbContext = dbContext;
    }
    public async Task<Account?> GetAccountByUserName(string username)
    {
        return _dbContext.Accounts.FirstOrDefault(x => username.Equals(x.Username));
    }

    public async Task<Account?> AddFunds(int userId, int amount)
    {
        var account = await _dbContext.Accounts.FindAsync(userId);
        if(account == null){ return null; }
        account.Balance += amount;
        await _dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<Account?> SubtractFunds(int userId, int amount)
    {
        var account = await _dbContext.Accounts.FindAsync(userId);
        if(account == null){ return null; }
        account.Balance -= amount;
        await _dbContext.SaveChangesAsync();
        return account;
    }
}