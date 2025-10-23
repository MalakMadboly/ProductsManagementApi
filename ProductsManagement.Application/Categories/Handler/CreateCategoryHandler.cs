using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsManagement.Application.Common.Interfaces;
using ProductsManagement.Domain.Categories;
using ProductsManagement.Application.Categories.Commands;
namespace ProductsManagement.Application.Categories.Handler
{
    public class CreateCategoryHandler:IRequestHandler<CreateCategoryCommand,Guid>
    {
        private readonly ICategoryRepository _categoryRepository;   
        private readonly IAppDbContext _db;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IAppDbContext db)
        {
            _categoryRepository = categoryRepository;
            _db = db;
        }

        public async Task<Guid>Handle(CreateCategoryCommand request,CancellationToken cancellationToken)
        {
            var category = new Category(request.Name);
            _categoryRepository.Add(category);
            _db.SaveChangesAsync(cancellationToken);
            return category.Id;
        }
    }
}
