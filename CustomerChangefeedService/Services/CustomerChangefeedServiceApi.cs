using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerChangefeedService.Command.EventGridMessagePublisher;
using CustomerChangefeedService.Helper;
using MediatR;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CustomerChangefeedService.Services
{
    public class CustomerChangefeedServiceApi
    {
        private readonly IMediator _mediator;

        public CustomerChangefeedServiceApi(IMediator mediator)
        {
            _mediator = mediator;
        }
        [FunctionName("CustomerChangefeedServiceApi")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "CustomerDb",
            collectionName: "CustomerInfo",
            ConnectionStringSetting = "CosmosConnectionString",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> input,
            ILogger log)
        {

            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents Count " + input.Count);

                try
                {
                    foreach (var document in input)
                    {
                        var eventData = await _mediator.Send(
                                    ModelHelper.Map(log, $"Customer:{document.GetPropertyValue<Guid>("Id")}", "CustomerInfo", "1.0", document));


                        log.LogInformation($"Received Document: {eventData.Subject} - item from Customer.");
                    }
                }
                catch (Exception ex)
                {
                    log.LogInformation($"Failed to send message:{ex.Message}");
                    log.LogInformation($"InnerException Details:{ex.InnerException}");
                }
            }
        }
    }
}
