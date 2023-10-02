using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CustomerApiService.Models;
using MediatR;
using Azure.Core;
using AutoMapper;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using CustomerApiService.Command.CreateCustomer;
using FluentValidation;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CustomerApiService.Helper;
//using Microsoft.Azure.Documents.ChangeFeedProcessor.Logging;

namespace CustomerApiService.Services
{
    public class CustomerCreateServiceApi
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerCreateModel> _customerCreateValidator;
       
        public CustomerCreateServiceApi(IMediator mediator, IMapper mapper, IValidator<CustomerCreateModel> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _customerCreateValidator = validator;
        }

        [FunctionName("AddCustomerServiceApi")]
        public async Task<IActionResult> Run(
             [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,  ILogger log)
        {

            log.LogInformation(" Create Customer HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var input = JsonConvert.DeserializeObject<CustomerCreateModel>(requestBody);

            var customer = _mapper.Map<CustomerCreateModel>(input);

            var customerValidationResult = await _customerCreateValidator.ValidateAsync(customer);

            if (!customerValidationResult.IsValid)
            {
                return customerValidationResult.Map();
            }

            var cmd = customer.Map();
            
            cmd.Logger = log;

            var result = await _mediator.Send(cmd);

            log.LogInformation($"New customer was successfully created.");

            return result; ;

        }

      
    }
}
