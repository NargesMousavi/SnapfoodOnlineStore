using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain
{
    public interface IProductRepository
    {
        Task<ProductViewModel?> GetByIdAsync(int id);
        Task<ProductViewModel> AddAsync(ProductViewModel product);
        Task<ProductViewModel?> GetByTitleAsync(string title);
        Task<bool> IncreaseInventoryCountAsync(int id, int inventoryCount);
    }
}
