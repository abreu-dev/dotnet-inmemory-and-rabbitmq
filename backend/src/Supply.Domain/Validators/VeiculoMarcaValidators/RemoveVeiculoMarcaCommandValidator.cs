using FluentValidation;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoMarcaValidators
{
    public class RemoveVeiculoMarcaCommandValidator : CommandValidator<RemoveVeiculoMarcaCommand>
    {
        public RemoveVeiculoMarcaCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
