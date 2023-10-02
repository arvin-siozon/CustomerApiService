using CustomerApiService.Contracts;
using CustomerApiService.Domain.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Models
{
    public class Customer :  EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? BirthdayInEpoch { get; set; }
        public string Email { get; set; }
    }
}
