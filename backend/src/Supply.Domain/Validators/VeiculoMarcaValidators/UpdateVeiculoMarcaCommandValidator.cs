using FluentValidation;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoMarcaValidators
{
    public class UpdateVeiculoMarcaCommandValidator : CommandValidator<UpdateVeiculoMarcaCommand>
    {
        public UpdateVeiculoMarcaCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Nome").Message);
        }
    }
}
