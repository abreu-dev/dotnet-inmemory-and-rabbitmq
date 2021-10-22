using FluentValidation;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.Messaging;

namespace Supply.Domain.Validators.VeiculoMarcaValidators
{
    class AddVeiculoMarcaCommandValidator : CommandValidator<AddVeiculoMarcaCommand>
    {
        public AddVeiculoMarcaCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(DomainMessages.RequiredField.Format("Nome").Message);
        }
    }
}
