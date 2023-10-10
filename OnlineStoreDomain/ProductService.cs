using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;

        public ProductService(IProductRepository productRepo,IUserRepository userRepo)
        {
            _productRepo = productRepo;
            _userRepo = userRepo;
        }
        public async Task<ProductViewModel> Add(ProductViewModel product)
        {
            if (product.Title.Length > 40)
            {
                throw new ArgumentException("Product title must be less than 40 characters.");
            }
            //sql server handle race condition issure, but this block of code can be implemented as thread safe also.
            if (await _productRepo.GetByTitleAsync(product.Title) != null)
            {
                throw new ArgumentException("Product title must be unique.");
            }
            await _productRepo.AddAsync(product);
            return product;
        }

        public async Task<bool> IncreaseInventoryCountAsync(int id, int count)
        {
            return await _productRepo.IncreaseInventoryCountAsync(id,count);
        }

        public async Task<ProductViewModel> GetProductWithDiscountAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id) ?? throw new ArgumentException("Invalid product id.");
            product.Price *= (1 - (product.Discount / 100));
            return product;
        }

        public async Task<int> BuyProductAsync(int productId, int userId)
        {
            return  await _userRepo.BuyProductAsync( productId,userId);
        }
    }
}
