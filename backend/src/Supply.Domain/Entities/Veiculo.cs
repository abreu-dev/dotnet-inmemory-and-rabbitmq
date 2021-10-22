using Supply.Domain.Core.Messaging.Domain;
using System;

namespace Supply.Domain.Entities
{
    public class Veiculo : Entity
    {
        public string Placa { get; private set; }
        public bool Removed { get; private set; }

        public Veiculo(string placa)
        {
            Placa = placa;
        }

        public Veiculo(Guid id, string placa) : base(id)
        {
            Placa = placa;
        }

        public void UpdatePlaca(string placa)
        {
            Placa = placa;
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
