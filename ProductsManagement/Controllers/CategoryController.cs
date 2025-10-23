using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Application.Categories.Commands;
using ProductsManagement.Application.Categories.Queries;
using ProductsManagement.Domain.Categories;

namespace ProductsManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ Create Category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { Message = "Category created successfully", Id = id });
        }

        // ✅ Update Category
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("Mismatched category ID");

            await _mediator.Send(command);
            return Ok(new { Message = "Category updated successfully" });
        }

        // ✅ Delete Category
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return Ok(new { Message = "Category deleted successfully" });
        }

        // ✅ Get All Categories
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        // ✅ Get Category By Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }
    }
}

