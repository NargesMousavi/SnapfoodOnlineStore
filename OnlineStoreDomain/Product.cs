using System.ComponentModel.DataAnnotations;

namespace OnlineStore
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(39)]
        public string Title { get; set; }
        public int InventoryCount { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
    }
}
