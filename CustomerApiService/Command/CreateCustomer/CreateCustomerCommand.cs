using CustomerApiService.Contracts;
using CustomerApiService.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Command.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<IActionResult>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthdayInEpochStr { get; set; }
        public long? BirthdayInEpoch => long.TryParse(BirthdayInEpochStr, out long val) ? val : null;
        public string Email { get; set; }
        public ILogger Logger { get; set; }
    }
}
