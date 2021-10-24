using FluentValidation;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoModeloValidators
{
    class AddVeiculoModeloCommandValidator : CommandValidator<AddVeiculoModeloCommand>
    {
        public AddVeiculoModeloCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Nome").Message);

            RuleFor(x => x.VeiculoMarcaId)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("VeiculoMarcaId").Message);
        }
    }
}
