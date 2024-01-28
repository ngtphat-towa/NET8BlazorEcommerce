using Ecommerce.SharedLibrary.Contracts;
using Ecommerce.SharedLibrary.Models;
using Ecommerce.SharedLibrary.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct productRepository) : ControllerBase
    {
        private readonly IProduct _productService = productRepository;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(bool IsFeaturedProduct)
        {
            var products = await _productService.GetAllProducts(IsFeaturedProduct);

            return Ok(products);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceRepsonse>> AddProduct(Product model)
        {
            var result = await _productService.AddProduct(model);

            if (!result.Flag)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
