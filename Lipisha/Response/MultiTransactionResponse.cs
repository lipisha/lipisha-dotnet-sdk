using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class MultiTransactionResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public List<Transaction> transactions { get; set; }
    }
}
