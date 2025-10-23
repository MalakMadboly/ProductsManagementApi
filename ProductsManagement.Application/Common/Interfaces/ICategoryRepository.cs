using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsManagement.Domain.Categories;
namespace ProductsManagement.Application.Common.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        Task<Category?> GetByIdAsync(Guid id);
    }
}
