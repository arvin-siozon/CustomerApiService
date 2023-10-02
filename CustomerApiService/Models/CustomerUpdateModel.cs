using CustomerApiService.Contracts;
using CustomerApiService.Domain.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Models
{
    public class CustomerUpdateModel : EntityBase
    {
        [JsonProperty]
        public string FirstName { get; set; }
        [JsonProperty]
        public string LastName { get; set; }
        [JsonProperty("BirthdayInEpoch")]
        public string BirthdayInEpochStr { get; set; }
        [JsonProperty]
        public string Email { get; set; }
    }
}
