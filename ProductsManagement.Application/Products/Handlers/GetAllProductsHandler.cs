using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Application.Products.Queries;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Products.Handlers
{
    public class GetAllProductsHandler: IRequestHandler<GetAllProductsQuery, List<Product>>
    {

        private readonly IProductRepository _repository;
        public GetAllProductsHandler(IProductRepository repository )
        {
            _repository = repository;
        }
        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
            return await
                _repository.GetAllAsync();
            }
        
    }
}
