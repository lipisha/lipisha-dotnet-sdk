using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class CustomerResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public List<Customer> customers { get; set; }
    }
}
