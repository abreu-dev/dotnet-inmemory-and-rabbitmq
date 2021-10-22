using FluentValidation;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoValidators
{
    public class RemoveVeiculoCommandValidator : CommandValidator<RemoveVeiculoCommand>
    {
        public RemoveVeiculoCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);
        }
    }
}
