using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipisha.Response
{
    public class StatusResponse
    {

        private const int SUCCESSFUL = 0;

        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("status_code")]
        public int statusCode { get; set; }
        [JsonProperty("status_description")]
        public string statusDescription { get; set; }

        public bool isSuccessful()
        {
            return statusCode == SUCCESSFUL;
        }
    }
}
