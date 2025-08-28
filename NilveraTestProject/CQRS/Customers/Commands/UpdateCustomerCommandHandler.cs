using System.Text.Json;
using MediatR;
using NilveraTestProject.Data.Repository.Abstract;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Commands
{
    public class UpdateCustomerCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.JsonData))
            {
                try { JsonDocument.Parse(request.JsonData); }
                catch (JsonException)
                {
                    throw new ArgumentException("Geçersiz JSON verisi.", nameof(request.JsonData));
                }
            }

            var customer = new Customer
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                JsonData = request.JsonData
            };

            return await _customerRepository.UpdateCustomerAsync(customer);
        }
    }
}
