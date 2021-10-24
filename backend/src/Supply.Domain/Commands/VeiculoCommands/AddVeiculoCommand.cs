using FluentValidation.Results;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoValidators;
using System;

namespace Supply.Domain.Commands.VeiculoCommands
{
    public class AddVeiculoCommand : Command
    {
        public string Placa { get; }
        public Guid VeiculoModeloId { get; }

        public AddVeiculoCommand(string placa, Guid veiculoModeloId) : base(Guid.Empty)
        {
            Placa = placa;
            VeiculoModeloId = veiculoModeloId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
