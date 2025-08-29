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
                try { using var _ = JsonDocument.Parse(request.JsonData); }
                catch (JsonException)
                {
                    throw new ArgumentException("Geçersiz JSON verisi.", nameof(request.JsonData));
                }
            }

            var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);
            if (customer is null) return false;

            if (request.Name is not null) customer.Name = request.Name;
            if (request.Surname is not null) customer.Surname = request.Surname;
            if (request.Email is not null) customer.Email = request.Email;
            if (request.Phone is not null) customer.Phone = request.Phone;
            if (request.Address is not null) customer.Address = request.Address;
            if (request.JsonData is not null) customer.JsonData = request.JsonData;


            return await _customerRepository.UpdateCustomerAsync(customer);

        }
    }
}
