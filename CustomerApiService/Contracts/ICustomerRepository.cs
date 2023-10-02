using CustomerApiService.Models;
using Microsoft.Azure.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Contracts
{
    public interface ICustomerRepository : IAsyncRepository<Customer>
    {
        IEnumerable<Customer> GetCustomerByEmail(string email);  
    }
}
