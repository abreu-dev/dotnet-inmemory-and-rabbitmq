using Supply.Caching.Core.Data;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories.Cache
{
    public class VeiculoMarcaCacheRepository : CacheRepository<VeiculoMarcaCache>, IVeiculoMarcaCacheRepository
    {
        public VeiculoMarcaCacheRepository(SupplyCacheContext supplyCacheContext)
            : base(supplyCacheContext.VeiculoMarcaCache) { }
    }
}