using Supply.Domain.Core.Messaging.Domain;
using System;
using System.Collections.Generic;

namespace Supply.Domain.Entities
{
    public class VeiculoMarca : Entity
    {
        public string Nome { get; private set; }

        // EF Rel.
        public virtual ICollection<VeiculoModelo> VeiculoModelos { get; set; }

        public VeiculoMarca(string nome)
        {
            Nome = nome;
            VeiculoModelos = new List<VeiculoModelo>();
        }

        public VeiculoMarca(Guid id, string nome) : base(id)
        {
            Nome = nome;
            VeiculoModelos = new List<VeiculoModelo>();
        }

        public void UpdateNome(string nome)
        {
            Nome = nome;
        }
    }
}
