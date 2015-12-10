using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class BaseStatusResponse
    {
        [JsonProperty("status")]
        public StatusResponse statusResponse { get; set; }

        public bool isSuccessful()
        {
            return statusResponse.isSuccessful();
        }

        public int getStatusCode()
        {
            return statusResponse.statusCode;
        }

        public string getStatus()
        {
            return statusResponse.status;
        }

        public string getStatusDescription()
        {
            return statusResponse.statusDescription;
        }

    }
}
