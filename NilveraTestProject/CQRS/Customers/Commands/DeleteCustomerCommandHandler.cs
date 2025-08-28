using MediatR;
using NilveraTestProject.Data.Repository.Abstract;

namespace NilveraTestProject.CQRS.Customers.Commands
{
    public class DeleteCustomerCommandHandler(ICustomerRepository customerRepository) : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.DeleteCustomerAsync(request.Id);
        }
    }
}
