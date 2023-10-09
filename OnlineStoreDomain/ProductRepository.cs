using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineStoreContext _dcContext;

        public ProductRepository(OnlineStoreContext dbContext)
        {
            _dcContext = dbContext;
        }

        public List<ProductViewModel> Products { get ; set; }

        public Product Add(Product product)
        {
            var pro = _dcContext.Products.Add(product).Entity;
            _dcContext.SaveChanges();
            return pro;
        }

        public ProductViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel GetByTitle(string titile)
        {
            throw new NotImplementedException();
        }

        public void InitializeProductCache()
        {
            Products = new List<Product>();
            Product=_dcContext.Products.Select();
        }
    }
}
