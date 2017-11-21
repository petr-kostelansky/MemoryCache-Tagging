using System.Collections.Generic;

namespace DevCode4.MemoryCacheTagging
{
    public interface ICacheManager
    {
        string GetTagName<TEntityEnum>(TEntityEnum cacheEntity, int entityId);

        T GetValue<T>(string key);

        T GetValue<T>(string key, out bool cached);

        CacheItemWrapper GetValue(string key);

        void SetValue(string key, object obj);

        void SetValue(string key, object obj, double duration);

        void SetValue<TEntityEnum>(string key, object obj, double duration, TEntityEnum? cacheEntity, IEnumerable<int> entityIds)
            where TEntityEnum : struct;

        void Remove(string key);

        void RemoveByTag<TEntityEnum>(TEntityEnum cacheEntity, int entityId);

        void Clear();
    }
}
