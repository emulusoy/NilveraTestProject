using System.Data;
using Dapper;
using NilveraTestProject.Data.Repository.Abstract;
using NilveraTestProject.Entities;

namespace NilveraTestProject.Data.Repository.Concrete
{
    public class CustomerRepository(IDbConnection dbConnection) : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        //primary ctor kullandim bazi yerlerde yaptigim caselerde ve baska gordugum sirketlerde boyle kullanildigini gordum zaten otomatik boyle kullanim sagliyor daha kisa kod icin! not her yerde dikat etmedim!
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            var values = await _dbConnection.QueryAsync<Customer>("sp_GetAllCustomers", commandType: CommandType.StoredProcedure);
            return values.ToList();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<Customer>("sp_GetCustomerById", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var parameters = new
            {
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.Phone,
                customer.Address,
                customer.JsonData
            };
            int newId = await _dbConnection.QuerySingleAsync<int>("sp_AddCustomer", parameters, commandType: CommandType.StoredProcedure);
            customer.Id = newId;
            return customer;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var parameters = new
            {
                customer.Id,
                customer.Name, customer.Surname, 
                customer.Email, customer.Phone, customer.Address, customer.JsonData };
            var val = await _dbConnection.ExecuteAsync("sp_UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);

            return val > 0;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
           var val=await _dbConnection.ExecuteAsync("sp_DeleteCustomer", new { Id = id }, commandType: CommandType.StoredProcedure);
            return val > 0;
        }
    }
}
