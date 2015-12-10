using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class UserResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public User user { get; set; }
    }
}
