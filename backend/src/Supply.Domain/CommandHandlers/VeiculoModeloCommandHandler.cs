using FluentValidation.Results;
using MediatR;
using Supply.Domain.Commands.VeiculoModeloCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Entities;
using Supply.Domain.Events.VeiculoModeloEvents;
using Supply.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Domain.CommandHandlers
{
    public class VeiculoModeloCommandHandler : CommandHandler,
        IRequestHandler<AddVeiculoModeloCommand, ValidationResult>,
        IRequestHandler<UpdateVeiculoModeloCommand, ValidationResult>,
        IRequestHandler<RemoveVeiculoModeloCommand, ValidationResult>
    {
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVeiculoModeloRepository _veiculoModeloRepository;
        private readonly IVeiculoMarcaRepository _veiculoMarcaRepository;

        public VeiculoModeloCommandHandler(IMessageBrokerBus messageBrokerBus,
                                           IVeiculoModeloRepository VeiculoModeloRepository, 
                                           IVeiculoMarcaRepository veiculoMarcaRepository)
        {
            _messageBrokerBus = messageBrokerBus;
            _veiculoModeloRepository = VeiculoModeloRepository;
            _veiculoMarcaRepository = veiculoMarcaRepository;
        }

        public async Task<ValidationResult> Handle(AddVeiculoModeloCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculoModelo = new VeiculoModelo(request.Nome, request.VeiculoMarcaId);

            if ((await _veiculoModeloRepository.Search(x => x.Nome == veiculoModelo.Nome)).Any())
            {
                AddError(DomainMessages.AlreadyInUse.Format("Nome").Message);
                return ValidationResult;
            }

            if (!(await _veiculoMarcaRepository.Search(x => x.Id == veiculoModelo.VeiculoMarcaId)).Any())
            {
                AddError(DomainMessages.NotFound.Format("VeiculoMarcaId").Message);
                return ValidationResult;
            }

            _veiculoModeloRepository.Add(veiculoModelo);

            if (await Commit(_veiculoModeloRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoModeloAddedEvent(veiculoModelo.Id));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateVeiculoModeloCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculoModelo = await _veiculoModeloRepository.GetById(request.AggregateId);
            if (veiculoModelo == null)
            {
                AddError(DomainMessages.NotFound.Format("VeiculoModelo").Message);
                return ValidationResult;
            }

            if ((await _veiculoModeloRepository.Search(x => x.Nome == request.Nome && x.Id != request.AggregateId)).Any())
            {
                AddError(DomainMessages.AlreadyInUse.Format("Nome").Message);
                return ValidationResult;
            }

            if (!(await _veiculoMarcaRepository.Search(x => x.Id == veiculoModelo.VeiculoMarcaId)).Any())
            {
                AddError(DomainMessages.NotFound.Format("VeiculoMarcaId").Message);
                return ValidationResult;
            }

            veiculoModelo.UpdateNome(request.Nome);
            veiculoModelo.UpdateVeiculoMarcaId(request.VeiculoMarcaId);
            _veiculoModeloRepository.Update(veiculoModelo);

            if (await Commit(_veiculoModeloRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoModeloUpdatedEvent(veiculoModelo.Id));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(RemoveVeiculoModeloCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculoModelo = await _veiculoModeloRepository.GetByIdWithIncludes(request.AggregateId);
            if (veiculoModelo == null)
            {
                AddError(DomainMessages.NotFound.Format("VeiculoModelo").Message);
                return ValidationResult;
            }

            if (veiculoModelo.Veiculos.Any())
            {
                AddError(DomainMessages.InUseByAnotherEntity.Format("VeiculoModelo", "Veiculos").Message);
                return ValidationResult;
            }

            _veiculoModeloRepository.Remove(veiculoModelo);

            if (await Commit(_veiculoModeloRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoModeloRemovedEvent(request.AggregateId));
            }

            return ValidationResult;
        }
    }
}
