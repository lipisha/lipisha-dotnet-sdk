using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class User
    {
        [JsonProperty("user_name")]
        public string userName { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
    }
}
