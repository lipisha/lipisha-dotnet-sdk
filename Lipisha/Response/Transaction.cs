using System;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class Transaction
    {
        [JsonProperty("transaction")]
        public string transactionId { get; set; }
        [JsonProperty("transaction_type")]
        public string transactionType { get; set; }
        [JsonProperty("transaction_method")]
        public string transactionMethod { get; set; }
        [JsonProperty("transaction_date")]
        public DateTime transactionDate { get; set; }
        [JsonProperty("transaction_account_name")]
        public string transactionAccountName { get; set; }
        [JsonProperty("transaction_account_number")]
        public string transactionAccountNumber { get; set; }
        [JsonProperty("transaction_reference")]
        public string transactionReference { get; set; }
        [JsonProperty("transaction_amount")]
        public double transactionAmount { get; set; }
        [JsonProperty("transaction_status")]
        public string transactionStatus { get; set; }
        [JsonProperty("transaction_name")]
        public string transactionName { get; set; }
        [JsonProperty("transaction_mobile_number")]
        public string transactionMobileNumber { get; set; }
        [JsonProperty("transaction_email")]
        public string transactionEmail { get; set; }
    }
}
