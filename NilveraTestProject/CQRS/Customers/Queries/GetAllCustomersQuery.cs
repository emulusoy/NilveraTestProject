using MediatR;
using NilveraTestProject.Dtos;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Queries
{
    public class GetAllCustomersQuery:IRequest<List<CustomerDto>>
    {
    }
}
