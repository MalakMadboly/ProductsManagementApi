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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand,Unit>
    {
        private readonly ICategoryRepository _repository;
        private readonly IAppDbContext _db;

        public DeleteCategoryHandler(ICategoryRepository repository, IAppDbContext db)
        {
            _repository = repository;
            _db = db;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id);
            if (category == null)
                throw new KeyNotFoundException("Category not found");

            _repository.Delete(category);
            await _db.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
