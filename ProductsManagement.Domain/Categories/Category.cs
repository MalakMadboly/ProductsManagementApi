using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Domain.Categories
{
    public class Category:BaseEntity
    {
        public string Name { get; private set; }
        public int ProductCount { get; private set; }
        public Category(string name)
        {
            Name = name;
            ProductCount = 0;
        }
        public void IncrementCount()=> ProductCount++;
        
        public void DecrementCount()
        {
            if (ProductCount > 0)
                ProductCount--;
        }
        public void Rename(string name)=>Name = name;
        
    }
}
