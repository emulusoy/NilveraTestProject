using MediatR;

namespace NilveraTestProject.CQRS.Customers.Commands
{
    public class DeleteCustomerCommand:IRequest<bool>
    {
        public int Id { get; set; }

    }
}
