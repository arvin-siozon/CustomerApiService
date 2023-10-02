using Azure.Messaging.EventGrid;
using CustomerChangefeedService.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerChangefeedService.Command.EventGridMessagePublisher
{
    public class PublishEventGridMessageCommand : IRequest<EventGridEvent>
    {
        public ILogger Log { get; set; }
        public string Subject { get; set; }
        public string EventType { get; set; }
        public string DataVersion { get; set; }
        public object Data { get; set; }
    }
}
