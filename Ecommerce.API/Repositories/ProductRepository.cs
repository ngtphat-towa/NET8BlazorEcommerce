using Ecommerce.API.Data;
using Ecommerce.SharedLibrary.Contracts;
using Ecommerce.SharedLibrary.Models;
using Ecommerce.SharedLibrary.Response;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class ProductRepository(AppDbContext appDbContext) : IProduct
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<ServiceRepsonse> AddProduct(Product model)
        {
            if (model is null) return new ServiceRepsonse(false, "Product model is empty");
            var (flag, msg) = await CheckName(model.Name!);
            if (flag)
            {
                _appDbContext.Products.Add(model);
                await Commit();
                return new ServiceRepsonse(flag, "Product Saved");
            }
            return new ServiceRepsonse(flag, msg);
        }

        public async Task<List<Product>> GetAllProducts(bool IsFeaturedProduct = false)
        {
            if (IsFeaturedProduct)
            {
                return await _appDbContext.Products.Where(_ => _.IsFeatured).ToListAsync();
            }
            else
            {
                return await _appDbContext.Products.ToListAsync();
            }
        }

        private async Task<ServiceRepsonse> CheckName(string name)
        {
            var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Name!.ToLower()!.Equals(name.ToLower()));
            return product is null ? new ServiceRepsonse(true, null!) : new ServiceRepsonse(false, "Product already exits");
        }
        private async Task Commit() => await _appDbContext.SaveChangesAsync();
    }
}
