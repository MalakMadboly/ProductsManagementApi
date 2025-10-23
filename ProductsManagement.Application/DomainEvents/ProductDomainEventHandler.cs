using MediatR;
using ProductsManagement.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Common.Interfaces;


namespace ProductsManagement.Application.DomainEvents
{
    
    public class ProductCreatedHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly IAppDbContext _db;
        public ProductCreatedHandler(IAppDbContext db) => _db = db;

        public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == notification.CategoryId, cancellationToken);
            if (category != null)
            {
                var count = await _db.Products.CountAsync(p => EF.Property<System.Guid>(p, "CategoryId") == notification.CategoryId, cancellationToken);
                while (category.ProductCount < count) category.IncrementCount();
                while (category.ProductCount > count) category.DecrementCount();

                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
  

    public class ProductDeletedHandler : INotificationHandler<ProductDeletedDomainEvent>
    {
        private readonly IAppDbContext _db;
        public ProductDeletedHandler(IAppDbContext db) => _db = db;

        public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Id == notification.CategoryId, cancellationToken);
            if (category != null)
            {
                var count = await _db.Products.CountAsync(p => EF.Property<System.Guid>(p, "CategoryId") == notification.CategoryId, cancellationToken);
                while (category.ProductCount < count) category.IncrementCount();
                while (category.ProductCount > count) category.DecrementCount();

                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
