using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Domain.Infra;

namespace OnlineStore.Domain
{
    public class ProductService
    {
        private readonly IProductRepository _repo;

        private readonly  ICache _productCache;

        public ProductService(IProductRepository repo,ICache cache)
        {
            _repo = repo;
            //    =new OnlineStoreContext();
            //_context.Database.EnsureCreated();
            CacheProducts();
        }

        private void CacheProducts()
        {
            _productCache = _context.Products.ToDictionary(p => p.Id);
        }

        private void UpdateProductCache(Product product)
        {
            _productCache[product.Id] = product;
        }

        private Product GetProductById(int id)
        {
            if (_productCache.ContainsKey(id))
            {
                return _productCache[id];
            }

            var product = _repo.GetById(id);
                //_context.Products.FirstOrDefault(p => p.Id == id);

            if (product != null)
            {
                _productCache[id] = product;
                return product;
            }

            return null;
        }

        private void AddProduct(Product product)
        {
            if (product.Title.Length > 40)
            {
                throw new ArgumentException("Product title must be less than 40 characters.");
            }

            if (_repo.GetByTitle(product.Title)!=null)
            {
                throw new ArgumentException("Product title must be unique.");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            CacheProducts();
        }

        private void IncreaseInventoryCount(int id, int count)
        {
            var product = GetProductById(id);

            if (product == null)
            {
                throw new ArgumentException("Invalid product id.");
            }

            product.InventoryCount += count;
            UpdateProductCache(product);
            _context.SaveChanges();
        }

        private Product GetProductWithDiscount(int id)
        {
            var product = GetProductById(id);

            if (product == null)
            {
                throw new ArgumentException("Invalid product id.");
            }

            var discountedPrice = product.Price * (1 - (product.Discount / 100));

            return new Product
            {
                Id = product.Id,
                Title = product.Title,
                InventoryCount = product.InventoryCount,
                Price = discountedPrice,
                Discount = product.Discount
            };
        }

        private void BuyProduct(int productId, int userId)
        {
            var user = _context.Users.Include(u => u.Orders).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user id.");
            }

            var product = GetProductById(productId);

            if (product == null)
            {
                throw new ArgumentException("Invalid product id.");
            }

            if (product.InventoryCount <= 0)
            {
                throw new InvalidOperationException("The inventory count of the selected product is zero.");
            }

            var order = new Order
            {
                Product = product,
                CreationDate = DateTime.Now,
                Buyer = user
            };

            user.Orders.Add(order);

            product.InventoryCount--;

            UpdateProductCache(product);

            _context.SaveChanges();
            CacheProducts();
        }
    }
}
