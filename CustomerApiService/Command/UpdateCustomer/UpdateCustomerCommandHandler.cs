using AutoMapper;
using CustomerApiService.Contracts;
using CustomerApiService.Helper;
using CustomerApiService.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerApiService.Command.CreateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, IActionResult>
    {

        private readonly ICustomerRepository _customerRepository;
        private ILogger _logger;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<IActionResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger = request.Logger ?? throw new ArgumentNullException(nameof(request.Logger));

            var customer = request.Map();

            _logger.LogInformation($"Attempting to create a new customer...");

            var newCustomer = await _customerRepository.UpdateAsync(customer);

            _logger.LogInformation($"Customer is updated successfully!");

            return new OkObjectResult(newCustomer); 
        }
    }
}
