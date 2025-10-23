using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsManagement.Application.Products.Commands;
using ProductsManagement.Domain.Products;
using MediatR;
using System.Reflection;
using ProductsManagement.Application.Common.Interfaces;


namespace ProductsManagement.Application.Products.Handlers
{
    public class UpdateProductHandler:IRequestHandler<UpdateProductCommand,Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IAppDbContext _db;
        public UpdateProductHandler(IProductRepository repository, IAppDbContext db)
        {
            _repository = repository;
            _db = db;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            var NewPrice = new Price(request.PriceValue, request.Currency);
            product.Update(request.Name,NewPrice,request.CategoryId);

            _repository.Update(product);
            await _db.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
