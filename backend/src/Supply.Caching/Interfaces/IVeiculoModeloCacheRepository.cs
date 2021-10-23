using Supply.Caching.Entities;
using System;
using System.Collections.Generic;

namespace Supply.Caching.Interfaces
{
    public interface IVeiculoModeloCacheRepository
    {
        IEnumerable<VeiculoModeloCache> GetAll();
        VeiculoModeloCache GetById(Guid id);

        void Add(VeiculoModeloCache veiculoModeloCache);
        void Update(VeiculoModeloCache veiculoModeloCache);
        void Remove(Guid id);
    }
}
