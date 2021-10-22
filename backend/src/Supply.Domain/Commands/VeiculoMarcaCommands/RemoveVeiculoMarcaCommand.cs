using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoMarcaValidators;
using System;

namespace Supply.Domain.Commands.VeiculoMarcaCommands
{
    public class RemoveVeiculoMarcaCommand : Command
    {
        public RemoveVeiculoMarcaCommand(Guid id) : base(id) { }

        public override bool IsValid()
        {
            ValidationResult = new RemoveVeiculoMarcaCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
