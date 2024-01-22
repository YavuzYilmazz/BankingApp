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
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PaymentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<PaymentResponse>>> Get()
        {
            var operation = new GetAllPaymentQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<PaymentResponse>> Get(int id)
        {
            var operation = new GetPaymentByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<PaymentResponse>> Post([FromBody] PaymentRequest payment)
        {
            var operation = new CreatePaymentCommand(payment);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] PaymentRequest payment)
        {
            var operation = new UpdatePaymentCommand(id, payment);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeletePaymentCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}