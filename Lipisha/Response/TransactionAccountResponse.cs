using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class TransactionAccountResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public TransactionAccount transactionAccount { get; set; }
    }
}
