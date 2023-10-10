using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Domain.Utilities;

namespace OnlineStore.Domain
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineStoreContext _dbContext;
        private readonly MemoryCache _productCache;
        private readonly string _cacheName; // should be unique in project

        public ProductRepository(OnlineStoreContext dbContext)
        {
            _dbContext = dbContext;
            _productCache = new MemoryCache(
        new MemoryCacheOptions
        {
            SizeLimit = 1024
        });
            _cacheName = nameof(Product);
        }
        public async Task<ProductViewModel> AddAsync(ProductViewModel product)
        {
            var result = await _dbContext.Products.AddAsync(new Product
            {
                Title = product.Title,
                Price = product.Price,
                Discount = product.Discount,
                InventoryCount = product.InventoryCount,
            });
            var prod = result.Entity;
            _dbContext.SaveChanges();
            var vm = prod.ToViewModel();
            AddToProductCache(vm);
            return vm;
        }
        public async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            if (_productCache.TryGetValue(GetCacheKey(id), out ProductViewModel cachedProduct))
            {
                return cachedProduct;
            }

            var product = await _dbContext.Products.FindAsync(id);
            //_context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var vm = new ProductViewModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Discount = product.Discount,
                    InventoryCount = product.InventoryCount,
                };
                AddToProductCache(vm);
                return vm;
            }
            return null;
        }
        private void AddToProductCache(ProductViewModel product)
        {
            var cachedValue = _productCache.GetOrCreate(GetCacheKey(product.Id),
                  cacheEntry =>
                  {
                      cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(3);
                      cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
                      return product;
                  });
        }
        public async Task<ProductViewModel?> GetByTitleAsync(string title)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Title == title);
            //_context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                var vm = new ProductViewModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    Discount = product.Discount,
                    InventoryCount = product.InventoryCount,
                };
                return vm;
            }
            return null;
        }
        private string GetCacheKey(int id)
        {
            return _cacheName + "#" + id;
        }

        public async Task<bool> IncreaseInventoryCountAsync(int id, int inventoryCount)
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product != null)
            {
                product.InventoryCount = inventoryCount;
                _dbContext.SaveChanges();
                _productCache.Remove(GetCacheKey(product.Id));
                AddToProductCache(product.ToViewModel());
                return true;
            }
            return false;
        }
    }
}
