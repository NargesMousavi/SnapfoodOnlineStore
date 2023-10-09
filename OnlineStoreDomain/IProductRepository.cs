using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public interface IProductRepository
    {
        List<ProductViewModel> Products { get; set; }
        void InitializeProductCache();
        ProductViewModel GetById(int id);
        ProductViewModel GetByTitle(string titile);
        ProductViewModel Add(Product product);
    }
}
