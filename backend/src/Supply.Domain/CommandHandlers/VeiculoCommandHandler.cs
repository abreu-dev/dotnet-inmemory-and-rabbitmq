using FluentValidation.Results;
using MediatR;
using Supply.Domain.Commands.VeiculoCommands;
using Supply.Domain.Core.Domain;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Core.Messaging;
using Supply.Domain.Entities;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Domain.CommandHandlers
{
    public class VeiculoCommandHandler : CommandHandler,
        IRequestHandler<AddVeiculoCommand, ValidationResult>,
        IRequestHandler<UpdateVeiculoCommand, ValidationResult>,
        IRequestHandler<RemoveVeiculoCommand, ValidationResult>
    {
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVeiculoRepository _VeiculoRepository;

        public VeiculoCommandHandler(IMessageBrokerBus messageBrokerBus, 
                                     IVeiculoRepository VeiculoRepository)
        {
            _messageBrokerBus = messageBrokerBus;
            _VeiculoRepository = VeiculoRepository;
        }

        public async Task<ValidationResult> Handle(AddVeiculoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculo = new Veiculo(request.Placa);

            if ((await _VeiculoRepository.Search(x => x.Placa == veiculo.Placa)).Any())
            {
                AddError(DomainMessages.AlreadyInUse.Format("Placa").Message);
                return ValidationResult;
            }

            _VeiculoRepository.Add(veiculo);

            if (await Commit(_VeiculoRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoAddedEvent(veiculo.Id));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateVeiculoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculo = await _VeiculoRepository.GetById(request.AggregateId);
            if (veiculo == null)
            {
                AddError(DomainMessages.NotFound.Format("Veiculo").Message);
                return ValidationResult;
            }

            if ((await _VeiculoRepository.Search(x => x.Placa == request.Placa && x.Id != request.AggregateId)).Any())
            {
                AddError(DomainMessages.AlreadyInUse.Format("Placa").Message);
                return ValidationResult;
            }

            veiculo.UpdatePlaca(request.Placa);
            _VeiculoRepository.Update(veiculo);

            if (await Commit(_VeiculoRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoUpdatedEvent(veiculo.Id));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(RemoveVeiculoCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                return request.ValidationResult;
            }

            var veiculo = await _VeiculoRepository.GetById(request.AggregateId);
            if (veiculo == null)
            {
                AddError(DomainMessages.NotFound.Format("Veiculo").Message);
                return ValidationResult;
            }

            _VeiculoRepository.Remove(veiculo);

            if (await Commit(_VeiculoRepository.UnitOfWork))
            {
                await _messageBrokerBus.PublishEvent(new VeiculoRemovedEvent(request.AggregateId));
            }

            return ValidationResult;
        }
    }
}
