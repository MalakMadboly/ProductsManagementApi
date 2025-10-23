using Microsoft.EntityFrameworkCore;
using ProductsManagement.Domain.Categories;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Product>Products { get; }
        DbSet<Category>Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
