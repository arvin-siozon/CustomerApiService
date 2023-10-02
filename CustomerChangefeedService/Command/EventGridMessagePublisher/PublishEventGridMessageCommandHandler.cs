using CustomerChangefeedService.Models;
using MediatR;
using System;

using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventGrid;
using Azure;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CustomerChangefeedService.Helper;

namespace CustomerChangefeedService.Command.EventGridMessagePublisher
{
    public class PublishEventGridMessageCommandHandler : IRequestHandler<PublishEventGridMessageCommand, EventGridEvent>
    {
        private readonly string eventGridTopicEndpoint = Environment.GetEnvironmentVariable("EventGridTopicEndpoint");
        private readonly string eventGridAccessKey = Environment.GetEnvironmentVariable("EventGridAccessKey");
        private readonly IMapper _mapper;
        private ILogger _logger;

        public PublishEventGridMessageCommandHandler(IMapper mapper) 
        {;
            _mapper = mapper;
        }

        public async Task<EventGridEvent> Handle(PublishEventGridMessageCommand cmd, CancellationToken cancellationToken)
        {
            _logger = cmd.Log;

            //var eventGrid = cmd.CreateEventGridMessage();

            var eventGridEvent = new EventGridEvent(cmd.Subject, cmd.EventType, cmd.DataVersion, cmd.Data.ToString());
            
            var client = CreateClient();
            if (client != null)
            {
                await client.SendEventAsync(eventGridEvent);
                _logger.LogInformation($"EventGrid message succesfuly sent: {eventGridEvent.Subject}");
                return eventGridEvent;
            }
            _logger.LogInformation($"EventGridTopicEndpoint Value: {eventGridTopicEndpoint}");
            _logger.LogInformation($"EventGridAccessKey Value: {eventGridAccessKey}");
            _logger.LogInformation($"EventGrid message succesfuly sent: {eventGridEvent.Subject}");
            return null;
        }
        private EventGridPublisherClient CreateClient()
        {
            EventGridPublisherClient client;
            try
            {
                client = new EventGridPublisherClient(new Uri(eventGridTopicEndpoint), new AzureKeyCredential(eventGridAccessKey));
                _logger.LogInformation($"Client Creation Succesful: {client}");
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to create client: {ex.Message}");
                return null;
            }

        }
    }
}
