using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Domain.Shared
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification @event)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(@event);
        }

        public void ClearDomainEvents() => _domainEvents?.Clear();
    }
}