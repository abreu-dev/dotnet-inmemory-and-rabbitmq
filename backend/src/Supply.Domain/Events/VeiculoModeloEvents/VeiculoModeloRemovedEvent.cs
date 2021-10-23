using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoModeloEvents
{
    public class VeiculoModeloRemovedEvent : Event
    {
        public VeiculoModeloRemovedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
