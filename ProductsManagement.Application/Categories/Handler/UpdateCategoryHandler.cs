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
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;
        private readonly IAppDbContext _db;

        public UpdateCategoryHandler(ICategoryRepository repository, IAppDbContext db)
        {
            _repository = repository;
            _db = db;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get existing category
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null)
                throw new KeyNotFoundException("Category not found");

            // 2️⃣ Use the domain method instead of setting the property directly
            category.Rename(request.Name);

            // 3️⃣ Update in repository and save changes
            _repository.Update(category);
            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

