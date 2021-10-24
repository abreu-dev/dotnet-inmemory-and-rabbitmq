using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoModeloEvents
{
    public class VeiculoModeloUpdatedEvent : Event
    {
        public VeiculoModeloUpdatedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
