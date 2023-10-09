namespace OnlineStore.Domain.Infra
{
    public interface ICache
    {
        int Count { get; }
        string Name { get; }
        void Add(string key, object value);
        void Remove(string key);
        void Clear();
        void Delete(string key);
        void Set(string key, object value);
        void Update(string key, object value);
        void DeleteAll(string key);
        void UpdateAll(string key, object value);

    }
}