using System;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class TransactionAccount
    {
        [JsonProperty("transaction_account_type")]
        public int accountType { get; set; }
        [JsonProperty("transaction_account_number")]
        public string accountNumber { get; set; }
        [JsonProperty("transaction_account_name")]
        public string accountName { get; set; }
        [JsonProperty("transaction_account_manager")]
        public string accountManager { get; set; }
    }
}
