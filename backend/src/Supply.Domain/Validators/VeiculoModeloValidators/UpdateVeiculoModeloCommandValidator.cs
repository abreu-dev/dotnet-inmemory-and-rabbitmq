using FluentValidation;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoModeloValidators
{
    public class UpdateVeiculoModeloCommandValidator : CommandValidator<UpdateVeiculoModeloCommand>
    {
        public UpdateVeiculoModeloCommandValidator()
        {
            RuleFor(x => x.AggregateId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Id").Message);

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Nome").Message);

            RuleFor(x => x.VeiculoMarcaId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("VeiculoMarcaId").Message);
        }
    }
}
