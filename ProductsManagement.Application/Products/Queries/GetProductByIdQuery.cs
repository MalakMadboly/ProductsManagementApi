using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductsManagement.Domain.Products;
namespace ProductsManagement.Application.Products.Queries
{
    
        public record GetProductByIdQuery(Guid Id) : IRequest<Product?>;
    

}
