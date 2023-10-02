using CustomerApiService.Contracts;
using CustomerApiService.Infrastracture.Persistence;
using CustomerApiService.Models;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Infrastracture.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext) : base(dbContext)
        {
         
        }

        public  IEnumerable<Customer> GetCustomerByEmail(string email)
        {
            var customer =  _dbContext.Customers
                                   .Where(o => o.Email == email)
                                   .ToList();
            return customer;
        }
    }
}
