using CustomerChangefeedService.Command.EventGridMessagePublisher;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Documents;
using System.Text;
using System.Threading.Tasks;
using CustomerChangefeedService.Models;

namespace CustomerChangefeedService.Helper
{
    public static class ModelHelper
    {
        public static PublishEventGridMessageCommand Map(ILogger logger, string subject, string eventType, string dataVersion, Document data)
        {
            return new PublishEventGridMessageCommand
            {
                Log = logger,
                Subject = subject,
                EventType = eventType,
                DataVersion = dataVersion,
                Data = data
            };
        }

        //public static EventGridModel Map(this PublishEventGridMessageCommand cmd)
        //{
        //    return new EventGridModel
        //    {
        //        Subject = cmd.Subject,
        //        EventType = cmd.EventType,
        //        DataVersion = cmd.DataVersion,
        //        Data = cmd.Data
        //    };
        //}

    }
}
