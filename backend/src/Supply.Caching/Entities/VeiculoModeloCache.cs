using Supply.Caching.Core.Caching;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoModeloCache : CacheEntity
    {
        public string Nome { get; set; }

        public VeiculoMarcaCache VeiculoMarca { get; set; }

        public VeiculoModeloCache(Guid id, int codigo, string nome, Guid veiculoMarcaId, int veiculoMarcaCodigo, string veiculoMarcaNome)
        {
            Id = id.ToString();
            Codigo = codigo;
            Nome = nome;
            VeiculoMarca = new VeiculoMarcaCache(veiculoMarcaId, veiculoMarcaCodigo, veiculoMarcaNome);
        }
    }
}
