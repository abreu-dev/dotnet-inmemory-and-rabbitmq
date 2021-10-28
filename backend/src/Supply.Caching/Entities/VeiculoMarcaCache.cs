using Supply.Caching.Core.Caching;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoMarcaCache : CacheEntity
    {
        public string Nome { get; set; }

        public VeiculoMarcaCache(Guid id, string nome)
        {
            Id = id.ToString();
            Nome = nome;
        }
    }
}
