using Supply.Domain.Core.Messaging.Domain;
using System;
using System.Collections.Generic;

namespace Supply.Domain.Entities
{
    public class VeiculoModelo : Entity
    {
        public string Nome { get; private set; }
        public Guid VeiculoMarcaId { get; private set; }

        // EF Rel.
        public virtual VeiculoMarca VeiculoMarca { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }

        public VeiculoModelo(string nome, Guid veiculoMarcaId)
        {
            Nome = nome;
            VeiculoMarcaId = veiculoMarcaId;
            Veiculos = new List<Veiculo>();
        }

        public VeiculoModelo(Guid id, string nome, Guid veiculoMarcaId) : base(id)
        {
            Nome = nome;
            VeiculoMarcaId = veiculoMarcaId;
            Veiculos = new List<Veiculo>();
        }

        public void UpdateNome(string nome)
        {
            Nome = nome;
        }

        public void UpdateVeiculoMarcaId(Guid veiculoMarcaId)
        {
            VeiculoMarcaId = veiculoMarcaId;
        }
    }
}
