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
using FluentValidation;
using CustomerApiService.Helper;


namespace CustomerApiService.Services
{
    public class CustomerUpdateServiceApi
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<CustomerUpdateModel> _CustomerUpdateValidator;
        public CustomerUpdateServiceApi(IMediator mediator, IMapper mapper, IValidator<CustomerUpdateModel> validator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _CustomerUpdateValidator = validator;
        }

        [FunctionName("UpdateCustomerServiceApi")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = null)] HttpRequest req, ILogger log)
        {

            log.LogInformation("Update Customer HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var input = JsonConvert.DeserializeObject<CustomerUpdateModel>(requestBody);

            var customer = _mapper.Map<CustomerUpdateModel>(input);

            var customerValidationResult = await _CustomerUpdateValidator.ValidateAsync(customer);

            if (!customerValidationResult.IsValid)
            {
                return customerValidationResult.Map();
            }

            var cmd = customer.Map();

            cmd.Logger = log;

            var result = await _mediator.Send(cmd);

            log.LogInformation($"New customer was successfully created.");

            return result; 

        }
    }
}
