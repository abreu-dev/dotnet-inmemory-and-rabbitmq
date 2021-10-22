using Supply.Domain.Core.Messaging.Domain;
using System;

namespace Supply.Domain.Entities
{
    public class VeiculoMarca : Entity
    {
        public string Nome { get; private set; }
        public bool Removed { get; private set; }

        public VeiculoMarca(string nome)
        {
            Nome = nome;
        }

        public VeiculoMarca(Guid id, string nome) : base(id)
        {
            Nome = nome;
        }

        public void UpdateNome(string nome)
        {
            Nome = nome;
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
