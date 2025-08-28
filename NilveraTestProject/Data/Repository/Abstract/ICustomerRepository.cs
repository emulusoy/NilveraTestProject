using NilveraTestProject.Entities;

namespace NilveraTestProject.Data.Repository.Abstract
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync();

        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);

    }
}
