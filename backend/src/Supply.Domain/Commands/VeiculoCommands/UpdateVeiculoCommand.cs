using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoValidators;
using System;

namespace Supply.Domain.Commands.VeiculoCommands
{
    public class UpdateVeiculoCommand : Command
    {
        public string Placa { get; }
        public Guid VeiculoModeloId { get; }

        public UpdateVeiculoCommand(Guid id, string placa, Guid veiculoModeloId) : base(id)
        {
            Placa = placa;
            VeiculoModeloId = veiculoModeloId;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
