using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Application.Products.Commands;
using ProductsManagement.Domain.Categories;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProductsManagement.Domain.Events;


namespace ProductsManagement.Application.Products.Handlers
{

    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppDbContext _db;
        public CreateProductHandler(IProductRepository repository,ICategoryRepository categoryRepository, IAppDbContext db)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _db = db;
        }


        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found");

            var price = new Price(request.PriceValue, request.Currency);
            var product = new Product(request.Name, price, request.CategoryId);
            category.IncrementCount();

            product.AddDomainEvent(new ProductCreatedDomainEvent(product.Id,request.CategoryId));
            _repository.Add(product);
            await _db.SaveChangesAsync(cancellationToken); // will trigger domain events
            return product.Id;
        }
    }
}
