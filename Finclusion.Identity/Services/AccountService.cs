using Finclusion.Database.Models;
using Finclusion.Database.Contexts;

namespace Finclusion.Identity.Services;

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

    public async Task<Account> CreateAccount(string username)
    {
        var existingAccount = await GetAccountByUserName(username);
        if(existingAccount != null) { return existingAccount; }

        var account = new Account()
        {
            Username = username,
            Balance = 0
        };

        await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return account;        
    }
}