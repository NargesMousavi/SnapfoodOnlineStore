using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain;

namespace OnlineStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ProductViewModel> Get(int id)
        {
            return await _productService.GetProductWithDiscountAsync(id);
        }

        [HttpPost]
        public async Task<ProductViewModel> Add([FromBody] ProductViewModel productDto)
        {
            return await _productService.Add(productDto);
        }
        [HttpPut("{id}/IncreseInventory")]
        public async Task<bool> IncreseInventory(int id, int count)
        {
            return await _productService.IncreaseInventoryCountAsync(id, count);
        }
        [HttpPost("{productId}/Buy")]
        public async Task<int> Buy(int productId, int userId)
        {
            return await _productService.BuyProductAsync(productId, userId);
        }
    }
}
