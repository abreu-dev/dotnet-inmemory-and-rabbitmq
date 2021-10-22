using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoValidators;
using System;

namespace Supply.Domain.Commands.VeiculoCommands
{
    public class RemoveVeiculoCommand : Command
    {
        public RemoveVeiculoCommand(Guid id) : base(id) { }

        public override bool IsValid()
        {
            ValidationResult = new RemoveVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
