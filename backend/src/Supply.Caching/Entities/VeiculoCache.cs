using Supply.Caching.Core.Caching;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoCache : CacheEntity
    {
        public string Placa { get; set; }

        public VeiculoModeloCache VeiculoModelo { get; set; }

        public VeiculoCache(Guid id, int codigo, string placa, Guid veiculoModeloId, int veiculoModeloCodigo, string veiculoModeloNome, Guid veiculoMarcaId, int veiculoMarcaCodigo, string veiculoMarcaNome)
        {
            Id = id.ToString();
            Codigo = codigo;
            Placa = placa;
            VeiculoModelo = new VeiculoModeloCache(veiculoModeloId, veiculoModeloCodigo, veiculoModeloNome, veiculoMarcaId, veiculoMarcaCodigo, veiculoMarcaNome);
        }
    }
}
