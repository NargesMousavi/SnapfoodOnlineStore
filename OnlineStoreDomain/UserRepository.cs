using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineStoreContext _dbContext;
        private readonly IProductRepository _productRepo;

        public UserRepository(OnlineStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> BuyProductAsync(int productId, int userId)
        {
            var user=await _dbContext.Users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new ArgumentException("Invalid user id.");
            }
            //var product = await _productRepo.GetByIdAsync(productId);
            var product = await _dbContext.Products.FindAsync(productId);
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
            _dbContext.SaveChanges();
            return order.Id;
        }

        public Task<User?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
