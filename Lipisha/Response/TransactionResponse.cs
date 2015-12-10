using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class TransactionResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public Transaction transaction { get; set; }
    }
}
