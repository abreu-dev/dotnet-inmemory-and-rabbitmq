using Supply.Caching.Core.Data;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories.Cache
{
    public class VeiculoCacheRepository : CacheRepository<VeiculoCache>, IVeiculoCacheRepository
    {
        public VeiculoCacheRepository(SupplyCacheContext supplyCacheContext) 
            : base(supplyCacheContext.VeiculoCache) { }
    }
}
