using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Domain.Utilities
{
    public static class ProductExtentions
    {
        public static ProductViewModel ToViewModel(this Product prod)
        {
            if (prod == null) return null;
            return new ProductViewModel
            {
                Id = prod.Id,
                Title = prod.Title,
                Price = prod.Price,
                Discount = prod.Discount,
                InventoryCount = prod.InventoryCount,
            };
        }
        public static Product ToModel(this ProductViewModel prod)
        {
            if (prod == null) return null;
            return new Product
            {
                Id = prod.Id,
                Title = prod.Title,
                Price = prod.Price,
                Discount = prod.Discount,
                InventoryCount = prod.InventoryCount,
            };
        }
    }
}
