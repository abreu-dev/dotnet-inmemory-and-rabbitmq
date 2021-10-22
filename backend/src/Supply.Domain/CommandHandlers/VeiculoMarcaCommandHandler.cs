using FluentValidation.Results;
using MediatR;
using Supply.Domain.Commands.VeiculoMarcaCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Entities;
using Supply.Domain.Events.VeiculoMarcaEvents;
using Supply.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Domain.CommandHandlers
{
    public class VeiculoMarcaCommandHandler : CommandHandler,
        IRequestHandler<AddVeiculoMarcaCommand, ValidationResult>,
        IRequestHandler<UpdateVeiculoMarcaCommand, ValidationResult>,
        IRequestHandler<RemoveVeiculoMarcaCommand, ValidationResult>
    {
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVeiculoMarcaRepository _veiculoMarcaRepository;

        public VeiculoMarcaCommandHandler(IMessageBrokerBus messageBrokerBus, 
                                          IVeiculoMarcaRepository VeiculoMarcaRepository)
        {
            _messageBrokerBus = messageBrokerBus;
            _veiculoMarcaRepository = VeiculoMarcaRepository;
        }

        public async Task<ValidationResult> Handle(AddVeiculoMarcaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculoMarca = new VeiculoMarca(request.Nome);

            if ((await _veiculoMarcaRepository.Search(x => x.Nome == veiculoMarca.Nome)).Any())
            {
                AddError(DomainMessages.AlreadyInUse.Format("Nome").Message);
                return ValidationResult;
            }

            _veiculoMarcaRepository.Add(veiculoMarca);

            if (await Commit(_veiculoMarcaRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoMarcaAddedEvent(veiculoMarca.Id));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateVeiculoMarcaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculoMarca = await _veiculoMarcaRepository.GetById(request.AggregateId);
            if (veiculoMarca == null)
            {
                AddError(DomainMessages.NotFound.Format("VeiculoMarca").Message);
                return ValidationResult;
            }

            if ((await _veiculoMarcaRepository.Search(x => x.Nome == request.Nome && x.Id != request.AggregateId)).Any())
            {
                AddError(DomainMessages.AlreadyInUse.Format("Nome").Message);
                return ValidationResult;
            }

            veiculoMarca.UpdateNome(request.Nome);
            _veiculoMarcaRepository.Update(veiculoMarca);

            if (await Commit(_veiculoMarcaRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoMarcaUpdatedEvent(veiculoMarca.Id));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(RemoveVeiculoMarcaCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculoMarca = await _veiculoMarcaRepository.GetById(request.AggregateId);
            if (veiculoMarca == null)
            {
                AddError(DomainMessages.NotFound.Format("VeiculoMarca").Message);
                return ValidationResult;
            }

            _veiculoMarcaRepository.Remove(veiculoMarca);

            if (await Commit(_veiculoMarcaRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoMarcaRemovedEvent(request.AggregateId));
            }

            return ValidationResult;
        }
    }
}
