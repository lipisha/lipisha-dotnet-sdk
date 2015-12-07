using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class WithdrawalAccountResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public WithdrawalAccount withdrawalAccount;
    }
}
