using Finclusion.Database.Models;

namespace Finclusion.Identity.Services;

public interface IAccountService
{
    Task<Account?> GetAccountByUserName(string username);
    Task<Account> CreateAccount(string username);
}