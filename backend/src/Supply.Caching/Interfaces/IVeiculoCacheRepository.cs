using Supply.Caching.Entities;
using System;
using System.Collections.Generic;

namespace Supply.Caching.Interfaces
{
    public interface IVeiculoCacheRepository
    {
        IEnumerable<VeiculoCache> GetAll();
        VeiculoCache GetById(Guid id);

        void Add(VeiculoCache veiculoCache);
        void Update(VeiculoCache veiculoCache);
        void Remove(Guid id);
    }
}
