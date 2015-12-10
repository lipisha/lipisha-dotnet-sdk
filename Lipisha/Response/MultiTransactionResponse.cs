using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class MultiTransactionResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public List<Transaction> transactions { get; set; }
    }
}
