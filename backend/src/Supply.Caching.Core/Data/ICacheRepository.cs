using Supply.Caching.Core.Caching;
using System;
using System.Collections.Generic;

namespace Supply.Caching.Core.Data
{
    public interface ICacheRepository<TCacheEntity> where TCacheEntity : CacheEntity
    {
        IEnumerable<TCacheEntity> GetAll();
        TCacheEntity GetById(Guid id);

        void Add(TCacheEntity cacheEntity);
        void Update(TCacheEntity cacheEntity);
        void Remove(Guid id);
    }
}
