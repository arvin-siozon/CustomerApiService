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
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IActionResult>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private ILogger _logger;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        public async Task<IActionResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            _logger = request.Logger ?? throw new ArgumentNullException(nameof(request.Logger));

            var customer = request.Map();

            _logger.LogInformation($"Attempting to create a new customer...");

            var newCustomer = await _customerRepository.AddAsync(customer);

            _logger.LogInformation($"Customer {newCustomer.Id} is successfully created.");

            return new OkObjectResult(newCustomer); 
        }
    }
}
