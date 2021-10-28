using System;

namespace Supply.Domain.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public bool Removed { get; private set; }
        public int Codigo { get; private set; }

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
