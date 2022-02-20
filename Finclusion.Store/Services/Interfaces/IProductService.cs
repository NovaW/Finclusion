using Finclusion.Database.Models;

namespace Finclusion.Store.Services;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetProducts();

    public Task<Order> BuyProduct(string userName, int productId);
}