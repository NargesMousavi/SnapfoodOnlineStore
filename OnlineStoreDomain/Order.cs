namespace OnlineStore.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public DateTime CreationDate { get; set; }
        public int BuyerId {  get; set; }
        public User Buyer { get; set; }
    }
}
