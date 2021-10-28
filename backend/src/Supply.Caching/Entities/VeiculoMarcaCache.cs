using Supply.Caching.Core.Caching;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoMarcaCache : CacheEntity
    {
        public string Nome { get; set; }

        public VeiculoMarcaCache(Guid id, int codigo, string nome)
        {
            Id = id.ToString();
            Codigo = codigo;
            Nome = nome;
        }
    }
}
