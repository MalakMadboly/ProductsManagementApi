using MediatR;
using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Application.Products.Commands;
using ProductsManagement.Domain.Categories;
using ProductsManagement.Domain.Events;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Products.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAppDbContext _db;

        public DeleteProductHandler(IProductRepository repository, ICategoryRepository categoryRepository, IAppDbContext db)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _db = db;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            // Fetch the category
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category != null)
            {
                category.DecrementCount(); // OR category.ProductCount--;
               
            }

            product.AddDomainEvent(new ProductDeletedDomainEvent(request.Id, product.CategoryId));
            _repository.Delete(product);
            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

}
