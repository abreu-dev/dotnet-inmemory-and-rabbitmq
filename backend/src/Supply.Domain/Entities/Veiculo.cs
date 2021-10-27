using Supply.Domain.Core.Messaging.Domain;
using System;

namespace Supply.Domain.Entities
{
    public class Veiculo : Entity
    {
        public string Placa { get; private set; }
        public Guid VeiculoModeloId { get; private set; }

        // EF Rel.
        public virtual VeiculoModelo VeiculoModelo { get; set; }

        public Veiculo(string placa, Guid veiculoModeloId)
        {
            Placa = placa;
            VeiculoModeloId = veiculoModeloId;
        }

        public Veiculo(Guid id, string placa, Guid veiculoModeloId) : base(id)
        {
            Placa = placa;
            VeiculoModeloId = veiculoModeloId;
        }

        public void UpdatePlaca(string placa)
        {
            Placa = placa;
        }

        public void UpdateVeiculoModeloId(Guid veiculoModeloId)
        {
            VeiculoModeloId = veiculoModeloId;
        }
    }
}
