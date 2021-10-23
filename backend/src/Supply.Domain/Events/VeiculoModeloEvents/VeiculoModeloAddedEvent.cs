using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoModeloEvents
{
    public class VeiculoModeloAddedEvent : Event
    {
        public VeiculoModeloAddedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
