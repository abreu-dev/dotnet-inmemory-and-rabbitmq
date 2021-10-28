using MediatR;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Core.MessageBroker;
using Supply.Domain.Events.VeiculoMarcaEvents;
using Supply.Domain.Events.VeiculoModeloEvents;
using Supply.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Infra.Data.EventHandlers
{
    public class VeiculoMarcaEventHandler : 
        INotificationHandler<VeiculoMarcaAddedEvent>,
        INotificationHandler<VeiculoMarcaUpdatedEvent>,
        INotificationHandler<VeiculoMarcaRemovedEvent>
    {
        private readonly IMessageBrokerBus _messageBrokerBus;
        private readonly IVeiculoMarcaRepository _veiculoMarcaRepository;
        private readonly IVeiculoMarcaCacheRepository _veiculoMarcaCacheRepository;

        public VeiculoMarcaEventHandler(IMessageBrokerBus messageBrokerBus,
                                        IVeiculoMarcaRepository veiculoMarcaRepository,
                                        IVeiculoMarcaCacheRepository veiculoMarcaCacheRepository)
        {
            _messageBrokerBus = messageBrokerBus;
            _veiculoMarcaRepository = veiculoMarcaRepository;
            _veiculoMarcaCacheRepository = veiculoMarcaCacheRepository;
        }

        public async Task Handle(VeiculoMarcaAddedEvent notification, CancellationToken cancellationToken)
        {
            var veiculoMarca = await _veiculoMarcaRepository.GetById(notification.AggregateId);
            var veiculoMarcaCache = new VeiculoMarcaCache(veiculoMarca.Id, veiculoMarca.Codigo, veiculoMarca.Nome);

            _veiculoMarcaCacheRepository.Add(veiculoMarcaCache);
        }

        public async Task Handle(VeiculoMarcaUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var veiculoMarca = await _veiculoMarcaRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoMarcaCache = new VeiculoMarcaCache(veiculoMarca.Id, veiculoMarca.Codigo, veiculoMarca.Nome);

            _veiculoMarcaCacheRepository.Update(veiculoMarcaCache);

            foreach (var veiculoModelo in veiculoMarca.VeiculoModelos)
            {
                await _messageBrokerBus.PublishEvent(new VeiculoModeloUpdatedEvent(veiculoModelo.Id));
            }
        }

        public async Task Handle(VeiculoMarcaRemovedEvent notification, CancellationToken cancellationToken)
        {
            _veiculoMarcaCacheRepository.Remove(notification.AggregateId);
            await Task.CompletedTask;
        }
    }
}
