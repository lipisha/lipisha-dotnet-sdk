using Newtonsoft.Json;

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
