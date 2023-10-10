namespace OnlineStore.Domain
{
    public interface IUserRepository
    {
        Task<int> BuyProductAsync(int productId, int userId);
        Task<User?> GetByIdAsync(int id);

    }
}