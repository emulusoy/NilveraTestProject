using System.Text.Json;
using MediatR;
using NilveraTestProject.Data.Repository.Abstract;
using NilveraTestProject.Dtos;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Queries
{
    public class GetAllCustomersQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var result = new List<CustomerDto>(customers.Count);
            foreach (var c in customers)
            {
                CustomerExtra? extra = null;
                if (!string.IsNullOrWhiteSpace(c.JsonData))
                {
                    try
                    {
                        extra = JsonSerializer.Deserialize<CustomerExtra>(c.JsonData, _jsonOptions);
                    }
                    catch (JsonException)
                    {
                    }
                }

                result.Add(new CustomerDto(
                    c.Id, c.Name, c.Surname, c.Email, c.Phone, c.Address, c.JsonData, extra));
            }

            return result;
        }
        }
    }

