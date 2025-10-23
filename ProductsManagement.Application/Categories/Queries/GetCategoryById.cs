using MediatR;
using ProductsManagement.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Category?>;
}
