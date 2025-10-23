using System;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Products.Commands
{
   
       public record UpdateProductCommand(Guid Id,string Name, decimal PriceValue, string Currency, Guid CategoryId):IRequest<Unit>;
    
}
