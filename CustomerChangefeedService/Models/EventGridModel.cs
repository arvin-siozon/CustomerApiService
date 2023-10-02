using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerChangefeedService.Models
{
    public class EventGridModel
    {
        public string Subject { get; set; }
        public string EventType { get; set; }
        public string DataVersion { get; set; }
        public object Data { get; set; }
    }
}
