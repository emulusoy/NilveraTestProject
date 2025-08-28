using MediatR;
using NilveraTestProject.Data.Repository.Abstract;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Queries
{
    public class GetCustomerByIdQueryHandler(ICustomerRepository customerRepository) : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer= _customerRepository.GetCustomerByIdAsync(request.Id);
            return customer;
        }
    }
}
