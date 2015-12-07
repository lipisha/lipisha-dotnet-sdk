using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class WithdrawalAccount
    {
        [JsonProperty("transaction_account_bank_name")]
        public string bankName { get; set; }
        [JsonProperty("transaction_account_bank_branch")]
        public string bankBranch { get; set; }
        [JsonProperty("transaction_account_bank_address")]
        public string bankAddress { get; set; }
        [JsonProperty("transaction_account_swift_code")]
        public string swiftCode { get; set; }
    }
}
