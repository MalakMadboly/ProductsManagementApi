using Domain.Shared;
using MediatR;
using ProductsManagement.Domain.Categories;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Common.Interfaces;

namespace ProductsManagement.Infrastructure.Persistence
{
    public class AppdbContext :DbContext, IAppDbContext
    {
        private readonly IMediator _mediator;
        public DbSet<Product> Products {  get; set; }
        public DbSet<Category> Categories { get; set; }
        public AppdbContext(DbContextOptions<AppdbContext> options, IMediator mediator)
               : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);
                b.OwnsOne(typeof(Price), "Price"); // price owned
                b.Property<System.Guid>("CategoryId").IsRequired();
            });

            builder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.ProductCount).IsRequired();
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            var domainEntities = ChangeTracker.Entries<BaseEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents != null && e.DomainEvents.Any())
                .ToList();

            var events = domainEntities.SelectMany(e => e.DomainEvents).ToList();

            domainEntities.ForEach(e => e.ClearDomainEvents());

            foreach (var @event in events)
            {
                await _mediator.Publish(@event, cancellationToken);
            }

            return result;
        }
    }
}
