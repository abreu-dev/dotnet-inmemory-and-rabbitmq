using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoMarcaEvents
{
    public class VeiculoMarcaAddedEvent : Event
    {
        public VeiculoMarcaAddedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
