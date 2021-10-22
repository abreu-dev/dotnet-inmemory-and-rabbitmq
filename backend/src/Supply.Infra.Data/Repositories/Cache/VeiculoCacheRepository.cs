using MongoDB.Driver;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace Supply.Infra.Data.Repositories.Cache
{
    public class VeiculoCacheRepository : IVeiculoCacheRepository
    {
        private readonly SupplyCacheContext _supplyCacheContext;

        public VeiculoCacheRepository(SupplyCacheContext supplyCacheContext)
        {
            _supplyCacheContext = supplyCacheContext;
        }

        public IEnumerable<VeiculoCache> GetAll()
        {
            return _supplyCacheContext.VeiculoCache.Find(_ => true).ToList();
        }

        public VeiculoCache GetById(Guid id)
        {
            return _supplyCacheContext.VeiculoCache.Find(e => e.Id == id.ToString()).SingleOrDefault();
        }

        public void Add(VeiculoCache veiculoCache)
        {
            _supplyCacheContext.VeiculoCache.InsertOne(veiculoCache);
        }

        public void Update(VeiculoCache veiculoCache)
        {
            _supplyCacheContext.VeiculoCache.ReplaceOne(x => x.Id == veiculoCache.Id, veiculoCache);
        }

        public void Remove(Guid id)
        {
            _supplyCacheContext.VeiculoCache.DeleteOne(x => x.Id == id.ToString());
        }
    }
}
