using MongoDB.Driver;
using Supply.Caching.Core.Caching;
using System;
using System.Collections.Generic;

namespace Supply.Caching.Core.Data
{
    public abstract class CacheRepository<TCacheEntity> : ICacheRepository<TCacheEntity> where TCacheEntity : CacheEntity
    {
        private readonly IMongoCollection<TCacheEntity> _mongoCollection;

        protected CacheRepository(IMongoCollection<TCacheEntity> mongoCollection)
        {
            _mongoCollection = mongoCollection;
        }

        public IEnumerable<TCacheEntity> GetAll()
        {
            return _mongoCollection.Find(_ => true).ToList();
        }

        public TCacheEntity GetById(Guid id)
        {
            return _mongoCollection.Find(e => e.Id == id.ToString()).SingleOrDefault();
        }

        public void Add(TCacheEntity cacheEntity)
        {
            _mongoCollection.InsertOne(cacheEntity);
        }

        public void Update(TCacheEntity cacheEntity)
        {
            _mongoCollection.ReplaceOne(x => x.Id == cacheEntity.Id, cacheEntity);
        }

        public void Remove(Guid id)
        {
            _mongoCollection.DeleteOne(x => x.Id == id.ToString());
        }
    }
}
