using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Akbank.Base.Response;
using Akbank.Business.Cqrs;
using Akbank.Schema;

namespace Akbank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseCategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Get()
        {
            var operation = new GetAllExpenseCategoryQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<ExpenseCategoryResponse>> Get(int id)
        {
            var operation = new GetExpenseCategoryByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<ExpenseCategoryResponse>> Post([FromBody] ExpenseCategoryRequest expenseCategory)
        {
            var operation = new CreateExpenseCategoryCommand(expenseCategory);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] ExpenseCategoryRequest expenseCategory)
        {
            var operation = new UpdateExpenseCategoryCommand(id, expenseCategory);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteExpenseCategoryCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
