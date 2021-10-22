using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoEvents
{
    public class VeiculoUpdatedEvent : Event
    {
        public VeiculoUpdatedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
