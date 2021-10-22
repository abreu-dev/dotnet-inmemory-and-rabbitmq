using FluentValidation.Results;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoMarcaValidators;
using System;

namespace Supply.Domain.Commands.VeiculoMarcaCommands
{
    public class AddVeiculoMarcaCommand : Command
    {
        public string Nome { get; }

        public AddVeiculoMarcaCommand(string nome) : base(Guid.Empty)
        {
            Nome = nome;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVeiculoMarcaCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
