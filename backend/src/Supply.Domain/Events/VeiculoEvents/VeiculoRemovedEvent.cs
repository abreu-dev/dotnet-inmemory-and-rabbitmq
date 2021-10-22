using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoEvents
{
    public class VeiculoRemovedEvent : Event
    {
        public VeiculoRemovedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
