using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Domain.Categories;
using Microsoft.EntityFrameworkCore;
namespace ProductsManagement.Infrastructure.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppdbContext _context;

        public CategoryRepository(AppdbContext context)
        {
            _context = context; 
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
        }
    }
}
