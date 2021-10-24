using MediatR;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Domain.Events.VeiculoModeloEvents;
using Supply.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Infra.Data.EventHandlers
{
    public class VeiculoModeloEventHandler : 
        INotificationHandler<VeiculoModeloAddedEvent>,
        INotificationHandler<VeiculoModeloUpdatedEvent>,
        INotificationHandler<VeiculoModeloRemovedEvent>
    {
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVeiculoModeloRepository _veiculoModeloRepository;
        private readonly IVeiculoModeloCacheRepository _veiculoModeloCacheRepository;

        public VeiculoModeloEventHandler(IMessageBrokerBus messageBrokerBus, 
                                         IVeiculoModeloRepository veiculoModeloRepository, 
                                         IVeiculoModeloCacheRepository veiculoModeloCacheRepository)
        {
            _messageBrokerBus = messageBrokerBus;
            _veiculoModeloRepository = veiculoModeloRepository;
            _veiculoModeloCacheRepository = veiculoModeloCacheRepository;
        }

        public async Task Handle(VeiculoModeloAddedEvent notification, CancellationToken cancellationToken)
        {
            var veiculoModelo = await _veiculoModeloRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoModeloCache = new VeiculoModeloCache(veiculoModelo.Id, 
                                                            veiculoModelo.Nome, 
                                                            veiculoModelo.VeiculoMarcaId,
                                                            veiculoModelo.VeiculoMarca.Nome);

            _veiculoModeloCacheRepository.Add(veiculoModeloCache);
        }

        public async Task Handle(VeiculoModeloUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var veiculoModelo = await _veiculoModeloRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoModeloCache = new VeiculoModeloCache(veiculoModelo.Id,
                                                            veiculoModelo.Nome,
                                                            veiculoModelo.VeiculoMarcaId,
                                                            veiculoModelo.VeiculoMarca.Nome);

            _veiculoModeloCacheRepository.Update(veiculoModeloCache);

            foreach (var veiculo in veiculoModelo.Veiculos)
            {
                await _messageBrokerBus.PublishEvent(new VeiculoUpdatedEvent(veiculo.Id));
            }
        }

        public async Task Handle(VeiculoModeloRemovedEvent notification, CancellationToken cancellationToken)
        {
            _veiculoModeloCacheRepository.Remove(notification.AggregateId);
            await Task.CompletedTask;
        }
    }
}
