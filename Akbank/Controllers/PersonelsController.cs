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
    public class PersonelsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PersonelsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<PersonelResponse>>> Get()
        {
            var operation = new GetAllPersonelQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<PersonelResponse>> Get(int id)
        {
            var operation = new GetPersonelByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("ByParameters")]
        public async Task<ApiResponse<List<PersonelResponse>>> GetByParameter(
            [FromQuery] string? FirstName,
            [FromQuery] string? LastName)
        {
            var operation = new GetPersonelByParameterQuery(FirstName, LastName);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<PersonelResponse>> Post([FromBody] PersonelRequest personel)
        {
            var operation = new CreatePersonelCommand(personel);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] PersonelRequest personel)
        {
            var operation = new UpdatePersonelCommand(id, personel);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeletePersonelCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
