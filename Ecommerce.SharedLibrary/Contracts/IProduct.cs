using Ecommerce.SharedLibrary.Models;
using Ecommerce.SharedLibrary.Response;


namespace Ecommerce.SharedLibrary.Contracts
{
    public interface IProduct
    {
        Task<ServiceRepsonse> AddProduct(Product model);
        Task<List<Product>> GetAllProducts(bool IsFeaturedProduct = false);
    }
}
