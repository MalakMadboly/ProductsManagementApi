using Domain.Shared;
using ProductsManagement.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Domain.Products
{
   public class Product: BaseEntity
    {
        public string Name { get; private set; }
        public Guid CategoryId { get; private set; }
        public Price Price { get; private set; }

        private Product() { }
        public Product(string name, Price price, Guid categoryId) 
        {
            Name = name;
            CategoryId = categoryId;
            Price = price;
            AddDomainEvent(new ProductCreatedDomainEvent(this.Id, this.CategoryId));
        }
        public void Update(string name, Price price,Guid categoryId)
        {
            Name = name;
            Price = price;
            CategoryId = categoryId;
            AddDomainEvent(new ProductUpdatedDomainEvent(this.Id, this.CategoryId));
        }
        public void MarkDeleted()
        {
            AddDomainEvent(new ProductDeletedDomainEvent(this.Id, this.CategoryId));
        }
    }
}
