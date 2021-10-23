using FluentValidation.Results;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoModeloValidators;
using System;

namespace Supply.Domain.Commands.VeiculoModeloCommands
{
    public class AddVeiculoModeloCommand : Command
    {
        public string Nome { get; }
        public Guid VeiculoMarcaId { get; }

        public AddVeiculoModeloCommand(string nome, Guid veiculoMarcaId) : base(Guid.Empty)
        {
            Nome = nome;
            VeiculoMarcaId = veiculoMarcaId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVeiculoModeloCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
