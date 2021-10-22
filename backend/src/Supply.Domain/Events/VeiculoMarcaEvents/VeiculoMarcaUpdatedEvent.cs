using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoMarcaEvents
{
    public class VeiculoMarcaUpdatedEvent : Event
    {
        public VeiculoMarcaUpdatedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
