 using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace ProductsManagement.Domain.Events
{
    public class ProductCreatedDomainEvent : INotification
    {
        public Guid ProductId { get; }
        public Guid CategoryId { get; }

        public ProductCreatedDomainEvent(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
    public class ProductUpdatedDomainEvent : INotification
    {
        public Guid ProductId { get; }
        public Guid CategoryId { get; }

        public ProductUpdatedDomainEvent(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
    public class ProductDeletedDomainEvent : INotification
    {
        public Guid ProductId { get; }
        public Guid CategoryId { get; }

        public ProductDeletedDomainEvent(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
}
