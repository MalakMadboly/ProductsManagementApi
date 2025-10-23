using MediatR;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Products.Queries
{
    public record GetAllProductsQuery : IRequest
       <List<Product?>>;
   
}
