using System.Text.Json;
using MediatR;
using NilveraTestProject.Entities;

namespace NilveraTestProject.CQRS.Customers.Commands
{
    public sealed record UpdateCustomerCommand:IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? JsonData;
    }
}
