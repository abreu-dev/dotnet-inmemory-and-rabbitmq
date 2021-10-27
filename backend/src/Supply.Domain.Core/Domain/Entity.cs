using System;

namespace Supply.Domain.Core.Messaging.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public bool Removed { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
