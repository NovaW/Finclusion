using Finclusion.Database.Models;

namespace Finclusion.Accounts.Services;

public interface IAccountService
{
    Task<Account?> GetAccountByUserName(string username);
    Task<Account> AddFunds(int userId, int amount);
    Task<Account> SubtractFunds(int userId, int amount);
}