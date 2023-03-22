using Microsoft.Extensions.Caching.Memory;
using UserApi.Models.Entities;

namespace UserApi.Data
{
    public class UserCache : IUserCache
    {
        private readonly IMemoryCache _cache;

        private string GetCacheKey(int id) => $"User-{id}";

        public UserCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public User Get(int id)
        {
            User user; 
            _cache.TryGetValue(GetCacheKey(id), out user);
            return user;
        }

        public void Remove(int id)
        {
            _cache.Remove(GetCacheKey(id));
        }

        public void Set(User user)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1);

            _cache.Set(GetCacheKey(user.Id),
                user,
                cacheEntryOptions);
        }
    }
}
