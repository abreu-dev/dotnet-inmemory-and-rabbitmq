using Supply.Domain.Core.Messaging;
using System;

namespace Supply.Domain.Events.VeiculoMarcaEvents
{
    public class VeiculoMarcaRemovedEvent : Event
    {
        public VeiculoMarcaRemovedEvent(Guid aggregateId) : base(aggregateId) { }
    }
}
