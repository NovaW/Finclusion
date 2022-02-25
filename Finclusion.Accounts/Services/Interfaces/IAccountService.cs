using Finclusion.Database.Models;

namespace Finclusion.Accounts.Services;

public interface IAccountService
{
    Task<Account> AddFunds(string username, int amount);
    Task<Account> SubtractFunds(string username, int amount);
}