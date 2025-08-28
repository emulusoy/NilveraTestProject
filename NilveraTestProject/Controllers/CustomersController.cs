using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NilveraTestProject.CQRS.Customers.Commands;
using NilveraTestProject.CQRS.Customers.Queries;

namespace NilveraTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCustomersQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetCustomerByIdQuery { Id = id };
            var customer = await _mediator.Send(query);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerCommand command)
        {
            command.Id = id;
            try
            {
                var updated = await _mediator.Send(command);
                if (updated) return Ok("Successfully updated.");
                return NotFound("Customer not found or update failed.");
            }
            catch (ArgumentException ex) when (ex.ParamName == nameof(command.JsonData))
            {
                return BadRequest("Geçersiz JSON verisi.");
            }//yukarisi gpt ile yapilmistir
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mediator.Send(new DeleteCustomerCommand { Id = id });
            return Ok("Deleted Successfully");
        }
    }
}
