using OnlineStore.Domain;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int InventoryCount { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public ICollection<Order> Orders { get; set;}
    }
}
