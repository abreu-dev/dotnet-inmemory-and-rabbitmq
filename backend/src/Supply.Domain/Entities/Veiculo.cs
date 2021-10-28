using Supply.Domain.Core.Domain;
using System;

namespace Supply.Domain.Entities
{
    public class Veiculo : Entity
    {
        public string Placa { get; private set; }
        public DateTime DataAquisicao { get; private set; }
        public double ValorAquisicao { get; private set; }
        public Guid VeiculoModeloId { get; private set; }

        // EF Rel.
        public virtual VeiculoModelo VeiculoModelo { get; set; }

        public Veiculo(string placa, DateTime dataAquisicao, double valorAquisicao, Guid veiculoModeloId)
        {
            Placa = placa;
            DataAquisicao = dataAquisicao;
            ValorAquisicao = valorAquisicao;
            VeiculoModeloId = veiculoModeloId;
        }

        public Veiculo(Guid id, string placa, DateTime dataAquisicao, double valorAquisicao, Guid veiculoModeloId) : base(id)
        {
            Placa = placa;
            DataAquisicao = dataAquisicao;
            ValorAquisicao = valorAquisicao;
            VeiculoModeloId = veiculoModeloId;
        }

        public void UpdatePlaca(string placa)
        {
            Placa = placa;
        }

        public void UpdateDataAquisicao(DateTime dataAquisicao)
        {
            DataAquisicao = dataAquisicao;
        }

        public void UpdateValorAquisicao(double valorAquisicao)
        {
            ValorAquisicao = valorAquisicao;
        }

        public void UpdateVeiculoModeloId(Guid veiculoModeloId)
        {
            VeiculoModeloId = veiculoModeloId;
        }
    }
}
