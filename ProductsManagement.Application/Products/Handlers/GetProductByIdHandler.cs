using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsManagement.Application.Products.Queries;
using ProductsManagement.Domain.Products;
using MediatR;
using System.Reflection;
using ProductsManagement.Application.Common.Interfaces;


namespace ProductsManagement.Application.Products.Handlers
{
    public class GetProductByIdHandler: IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductsManagement.Domain.Products.Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}
