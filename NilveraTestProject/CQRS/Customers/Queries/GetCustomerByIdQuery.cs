using MediatR;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Queries
{
    public class GetCustomerByIdQuery:IRequest<Customer>
    {
        public int Id { get; set; }

    }
}
