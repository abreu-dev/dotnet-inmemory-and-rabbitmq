using FluentValidation;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoModeloValidators
{
    public class RemoveVeiculoModeloCommandValidator : CommandValidator<RemoveVeiculoModeloCommand>
    {
        public RemoveVeiculoModeloCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
