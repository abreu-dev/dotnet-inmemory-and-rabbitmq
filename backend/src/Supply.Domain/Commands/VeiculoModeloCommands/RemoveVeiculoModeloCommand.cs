using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoModeloValidators;
using System;

namespace Supply.Domain.Commands.VeiculoModeloCommands
{
    public class RemoveVeiculoModeloCommand : Command
    {
        public RemoveVeiculoModeloCommand(Guid id) : base(id) { }

        public override bool IsValid()
        {
            ValidationResult = new RemoveVeiculoModeloCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
