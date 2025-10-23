using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductsManagement.Infrastructure.Persistence
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppdbContext _db;
        public ProductRepository(AppdbContext db) => _db = db;
       public async Task<List<Product>> GetAllAsync()=>await _db.Products.ToListAsync();
        public async Task<Product>GetByIdAsync (Guid id)=>await _db.Products.FirstOrDefaultAsync(p=>p.Id==id);
        public void Add(Product product)=>_db.Products.Add(product);
        public void Update (Product product)=>_db.Products.Update(product);
        public void Delete (Product product)=>_db.Remove(product);
    }
}
