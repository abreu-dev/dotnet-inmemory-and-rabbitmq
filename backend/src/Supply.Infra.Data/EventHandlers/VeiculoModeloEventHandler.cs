using MediatR;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
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
        private readonly IVeiculoModeloRepository _veiculoModeloRepository;
        private readonly IVeiculoModeloCacheRepository _veiculoModeloCacheRepository;

        public VeiculoModeloEventHandler(IVeiculoModeloRepository veiculoModeloRepository, 
                                         IVeiculoModeloCacheRepository veiculoModeloCacheRepository)
        {
            _veiculoModeloRepository = veiculoModeloRepository;
            _veiculoModeloCacheRepository = veiculoModeloCacheRepository;
        }

        public async Task Handle(VeiculoModeloAddedEvent notification, CancellationToken cancellationToken)
        {
            var veiculoModelo = await _veiculoModeloRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoModeloCache = new VeiculoModeloCache(veiculoModelo.Id, 
                                                            veiculoModelo.Nome, 
                                                            veiculoModelo.VeiculoMarcaId,
                                                            veiculoModelo.VeiculoMarca?.Nome);

            _veiculoModeloCacheRepository.Add(veiculoModeloCache);
        }

        public async Task Handle(VeiculoModeloUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var veiculoModelo = await _veiculoModeloRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoModeloCache = new VeiculoModeloCache(veiculoModelo.Id,
                                                            veiculoModelo.Nome,
                                                            veiculoModelo.VeiculoMarcaId,
                                                            veiculoModelo.VeiculoMarca?.Nome);

            _veiculoModeloCacheRepository.Update(veiculoModeloCache);
        }

        public async Task Handle(VeiculoModeloRemovedEvent notification, CancellationToken cancellationToken)
        {
            _veiculoModeloCacheRepository.Remove(notification.AggregateId);
            await Task.CompletedTask;
        }
    }
}
