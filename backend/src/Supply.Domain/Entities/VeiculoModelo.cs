using Supply.Domain.Core.Messaging.Domain;
using System;

namespace Supply.Domain.Entities
{
    public class VeiculoModelo : Entity
    {
        public string Nome { get; private set; }
        public bool Removed { get; private set; }
        public Guid VeiculoMarcaId { get; private set; }

        // EF Rel.
        public virtual VeiculoMarca VeiculoMarca { get; set; }

        public VeiculoModelo(string nome, Guid veiculoMarcaId)
        {
            Nome = nome;
            VeiculoMarcaId = veiculoMarcaId;
        }

        public VeiculoModelo(Guid id, string nome, Guid veiculoMarcaId) : base(id)
        {
            Nome = nome;
            VeiculoMarcaId = veiculoMarcaId;
        }

        public void UpdateNome(string nome)
        {
            Nome = nome;
        }

        public void UpdateVeiculoMarcaId(Guid veiculoMarcaId)
        {
            VeiculoMarcaId = veiculoMarcaId;
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
