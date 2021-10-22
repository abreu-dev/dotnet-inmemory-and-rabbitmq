using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoValidators;
using System;

namespace Supply.Domain.Commands.VeiculoCommands
{
    public class UpdateVeiculoCommand : Command
    {
        public string Placa { get; }

        public UpdateVeiculoCommand(Guid id, string placa) : base(id)
        {
            Placa = placa;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
