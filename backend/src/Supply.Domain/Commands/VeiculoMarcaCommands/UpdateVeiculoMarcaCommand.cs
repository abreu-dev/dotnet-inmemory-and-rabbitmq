using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoMarcaValidators;
using System;

namespace Supply.Domain.Commands.VeiculoMarcaCommands
{
    public class UpdateVeiculoMarcaCommand : Command
    {
        public string Nome { get; }

        public UpdateVeiculoMarcaCommand(Guid id, string nome) : base(id)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateVeiculoMarcaCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
