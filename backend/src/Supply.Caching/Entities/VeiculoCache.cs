using Supply.Caching.Core.Caching;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoCache : CacheEntity
    {
        public string Placa { get; set; }

        public VeiculoModeloCache VeiculoModelo { get; set; }

        public VeiculoCache(Guid id, string placa, Guid veiculoModeloId, string veiculoModeloNome, Guid veiculoMarcaId, string veiculoMarcaNome)
        {
            Id = id.ToString();
            Placa = placa;
            VeiculoModelo = new VeiculoModeloCache(veiculoModeloId, veiculoModeloNome, veiculoMarcaId, veiculoMarcaNome);
        }
    }
}
