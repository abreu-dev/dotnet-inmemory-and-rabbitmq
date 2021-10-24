using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoModeloValidators;
using System;

namespace Supply.Domain.Commands.VeiculoModeloCommands
{
    public class UpdateVeiculoModeloCommand : Command
    {
        public string Nome { get; }
        public Guid VeiculoMarcaId { get; }

        public UpdateVeiculoModeloCommand(Guid id, string nome, Guid veiculoMarcaId) : base(id)
        {
            Nome = nome;
            VeiculoMarcaId = veiculoMarcaId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateVeiculoModeloCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
