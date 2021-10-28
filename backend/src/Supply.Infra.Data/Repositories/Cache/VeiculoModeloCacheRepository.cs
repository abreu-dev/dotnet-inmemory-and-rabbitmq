using Supply.Caching.Core.Data;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Infra.Data.Context;

namespace Supply.Infra.Data.Repositories.Cache
{
    public class VeiculoModeloCacheRepository : CacheRepository<VeiculoModeloCache>, IVeiculoModeloCacheRepository
    {
        public VeiculoModeloCacheRepository(SupplyCacheContext supplyCacheContext)
            : base(supplyCacheContext.VeiculoModeloCache) { }
    }
}