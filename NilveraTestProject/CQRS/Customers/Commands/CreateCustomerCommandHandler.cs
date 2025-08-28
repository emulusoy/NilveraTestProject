using System.Text.Json;
using MediatR;
using NilveraTestProject.Data.Repository.Abstract;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Commands
{
    public class CreateCustomerCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<CreateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var extra = new CustomerExtra
            {
                RegistrationDate = DateTime.UtcNow,
                Source = "Web API"
            };
            var customer = new Customer
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                JsonData = JsonSerializer.Serialize(extra, _jsonOptions)
            };
            //jsnon data ekleme
            return await _customerRepository.CreateCustomerAsync(customer);

        }
    }
}
