using Finclusion.Database.Models;
using Finclusion.Database.Contexts;

namespace Finclusion.Store.Services;

public class ProductService : IProductService
{
    private readonly FinclusionContext _dbContext;
    public ProductService(FinclusionContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return _dbContext.Products.Select(x => x).AsEnumerable();
    }

    public async Task<Order> BuyProduct(string userName, int productId)
    {
        var account = _dbContext.Accounts.FirstOrDefault(x => x.Username.Equals(userName));
        var product = _dbContext.Products.FirstOrDefault(x => x.Id == productId);
        if(account == null || product == null) { return null; }

        if(account.Balance < product.Cost)
        {
            throw new InvalidOperationException($"Insufficient funds to buy {product.ProductName}. Cost is {product.Cost}, account balance is {account.Balance}."); 
        }

        account.Balance -= product.Cost;
        
        if(product.Quantity == 0)
        {
            throw new InvalidOperationException($"${product.ProductName} out of stock.");
        }

        product.Quantity--;
        
        var order = new Order()
        {
            AccountId = account.Id,
            ProductId = productId,
            DateOrdered = DateTime.UtcNow
        };

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }
}