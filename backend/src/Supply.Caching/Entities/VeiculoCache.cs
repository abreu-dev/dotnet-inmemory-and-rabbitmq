using Supply.Caching.Core.Caching;
using System;

namespace Supply.Caching.Entities
{
    public class VeiculoCache : CacheEntity
    {
        public string Placa { get; set; }
        public DateTime DataAquisicao { get; set; }
        public double ValorAquisicao { get; set; }
        public VeiculoModeloCache VeiculoModelo { get; set; }

        public VeiculoCache(Guid id, int codigo, string placa, DateTime dataAquisicao, double valorAquisicao, Guid veiculoModeloId, int veiculoModeloCodigo, string veiculoModeloNome, Guid veiculoMarcaId, int veiculoMarcaCodigo, string veiculoMarcaNome)
        {
            Id = id.ToString();
            Codigo = codigo;
            Placa = placa;
            DataAquisicao = dataAquisicao;
            ValorAquisicao = valorAquisicao;
            VeiculoModelo = new VeiculoModeloCache(veiculoModeloId, veiculoModeloCodigo, veiculoModeloNome, veiculoMarcaId, veiculoMarcaCodigo, veiculoMarcaNome);
        }
    }
}
