using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProductsManagement.Application.Products.Commands
{
   public record CreateProductCommand(string Name, decimal PriceValue, string Currency, Guid CategoryId):IRequest<Guid>;
}
