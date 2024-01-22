using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Akbank.Base.Response;
using Akbank.Business.Cqrs;
using Akbank.Data.Entity;

namespace Akbank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseRequestsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExpenseRequestsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ExpenseRequestResponse>>> Get()
        {
            var operation = new GetAllExpenseRequestQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<ExpenseRequestResponse>> Get(int id)
        {
            var operation = new GetExpenseRequestByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByParameters")]
        public async Task<ApiResponse<List<ExpenseRequestResponse>>> GetByParameter(
            [FromQuery] decimal? RequestAmount,
            [FromQuery] bool? Approved)
        {
            var operation = new GetExpenseRequestByParameterQuery(RequestAmount ?? 0, Approved ?? false);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpPost]
        public async Task<ApiResponse<ExpenseRequestResponse>> Post([FromBody] ExpenseRequestRequest expenseRequest)
        {
            var operation = new CreateExpenseRequestCommand(expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] ExpenseRequestRequest expenseRequest)
        {
            var operation = new UpdateExpenseRequestCommand(id, expenseRequest);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteExpenseRequestCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
