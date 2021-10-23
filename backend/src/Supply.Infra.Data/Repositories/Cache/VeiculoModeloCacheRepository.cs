using MongoDB.Driver;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;
using System;
using System.Collections.Generic;

namespace Supply.Infra.Data.Repositories.Cache
{
    public class VeiculoModeloCacheRepository : IVeiculoModeloCacheRepository
    {
        private readonly SupplyCacheContext _supplyCacheContext;

        public VeiculoModeloCacheRepository(SupplyCacheContext supplyCacheContext)
        {
            _supplyCacheContext = supplyCacheContext;
        }

        public IEnumerable<VeiculoModeloCache> GetAll()
        {
            return _supplyCacheContext.VeiculoModeloCache.Find(_ => true).ToList();
        }

        public VeiculoModeloCache GetById(Guid id)
        {
            return _supplyCacheContext.VeiculoModeloCache.Find(e => e.Id == id.ToString()).SingleOrDefault();
        }

        public void Add(VeiculoModeloCache veiculoModeloCache)
        {
            _supplyCacheContext.VeiculoModeloCache.InsertOne(veiculoModeloCache);
        }

        public void Update(VeiculoModeloCache veiculoModeloCache)
        {
            _supplyCacheContext.VeiculoModeloCache.ReplaceOne(x => x.Id == veiculoModeloCache.Id, veiculoModeloCache);
        }

        public void Remove(Guid id)
        {
            _supplyCacheContext.VeiculoModeloCache.DeleteOne(x => x.Id == id.ToString());
        }
    }
}
