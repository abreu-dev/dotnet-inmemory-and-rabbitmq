using MediatR;
using Supply.Caching.Entities;
using Supply.Caching.Interfaces;
using Supply.Domain.Events.VeiculoEvents;
using Supply.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Supply.Infra.Data.EventHandlers
{
    public class VeiculoEventHandler : 
        INotificationHandler<VeiculoAddedEvent>,
        INotificationHandler<VeiculoUpdatedEvent>,
        INotificationHandler<VeiculoRemovedEvent>
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IVeiculoCacheRepository _veiculoCacheRepository;

        public VeiculoEventHandler(IVeiculoRepository veiculoRepository, 
                                   IVeiculoCacheRepository VeiculoCacheRepository)
        {
            _veiculoRepository = veiculoRepository;
            _veiculoCacheRepository = VeiculoCacheRepository;
        }

        public async Task Handle(VeiculoAddedEvent notification, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoCache = new VeiculoCache(veiculo.Id,
                                                veiculo.Codigo,
                                                veiculo.Placa,
                                                veiculo.VeiculoModeloId,
                                                veiculo.VeiculoModelo.Codigo,
                                                veiculo.VeiculoModelo.Nome,
                                                veiculo.VeiculoModelo.VeiculoMarcaId,
                                                veiculo.VeiculoModelo.Codigo,
                                                veiculo.VeiculoModelo.VeiculoMarca.Nome);

            _veiculoCacheRepository.Add(veiculoCache);
        }

        public async Task Handle(VeiculoUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var veiculo = await _veiculoRepository.GetByIdWithIncludes(notification.AggregateId);
            var veiculoCache = new VeiculoCache(veiculo.Id,
                                                veiculo.Codigo,
                                                veiculo.Placa,
                                                veiculo.VeiculoModeloId,
                                                veiculo.VeiculoModelo.Codigo,
                                                veiculo.VeiculoModelo.Nome,
                                                veiculo.VeiculoModelo.VeiculoMarcaId,
                                                veiculo.VeiculoModelo.Codigo,
                                                veiculo.VeiculoModelo.VeiculoMarca.Nome);

            _veiculoCacheRepository.Update(veiculoCache);
        }

        public async Task Handle(VeiculoRemovedEvent notification, CancellationToken cancellationToken)
        {
            _veiculoCacheRepository.Remove(notification.AggregateId);
            await Task.CompletedTask;
        }
    }
}
