using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories.Cache
{
    public class VeiculoMarcaCacheRepository : IVeiculoMarcaCacheRepository
    {
        private readonly SupplyCacheContext _supplyCacheContext;

        public VeiculoMarcaCacheRepository(SupplyCacheContext supplyCacheContext)
        {
            _supplyCacheContext = supplyCacheContext;
        }

        public IEnumerable<VeiculoMarcaCache> GetAll()
        {
            return _supplyCacheContext.VeiculoMarcaCache.Find(_ => true).ToList();
        }

        public VeiculoMarcaCache GetById(Guid id)
        {
            return _supplyCacheContext.VeiculoMarcaCache.Find(e => e.Id == id.ToString()).SingleOrDefault();
        }

        public void Add(VeiculoMarcaCache veiculoMarcaCache)
        {
            _supplyCacheContext.VeiculoMarcaCache.InsertOne(veiculoMarcaCache);
        }

        public void Update(VeiculoMarcaCache veiculoMarcaCache)
        {
            _supplyCacheContext.VeiculoMarcaCache.ReplaceOne(x => x.Id == veiculoMarcaCache.Id, veiculoMarcaCache);
        }

        public void Remove(Guid id)
        {
            _supplyCacheContext.VeiculoMarcaCache.DeleteOne(x => x.Id == id.ToString());
        }
    }
}
