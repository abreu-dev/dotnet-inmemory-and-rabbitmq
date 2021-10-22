using Supply.Caching.Entities;
using System;
using System.Collections.Generic;

namespace Supply.Caching.Interfaces
{
    public interface IVeiculoMarcaCacheRepository
    {
        IEnumerable<VeiculoMarcaCache> GetAll();
        VeiculoMarcaCache GetById(Guid id);

        void Add(VeiculoMarcaCache veiculoMarcaCache);
        void Update(VeiculoMarcaCache veiculoMarcaCache);
        void Remove(Guid id);
    }
}
