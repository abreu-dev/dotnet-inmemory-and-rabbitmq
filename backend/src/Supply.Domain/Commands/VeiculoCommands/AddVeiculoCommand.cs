using FluentValidation.Results;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Validators.VeiculoValidators;
using System;

namespace Supply.Domain.Commands.VeiculoCommands
{
    public class AddVeiculoCommand : Command
    {
        public string Placa { get; }
        public DateTime DataAquisicao { get; }
        public double ValorAquisicao { get; }
        public Guid VeiculoModeloId { get; }

        public AddVeiculoCommand(string placa, DateTime dataAquisicao, double valorAquisicao, Guid veiculoModeloId) : base(Guid.Empty)
        {
            Placa = placa;
            DataAquisicao = dataAquisicao;
            ValorAquisicao = valorAquisicao;
            VeiculoModeloId = veiculoModeloId;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddVeiculoCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
