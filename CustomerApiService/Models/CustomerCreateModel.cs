using CustomerApiService.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Models
{
    public class CustomerCreateModel 
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
