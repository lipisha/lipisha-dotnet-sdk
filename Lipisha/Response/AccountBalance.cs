namespace Lipisha.Response
{
    public class AccountBalance : BaseResponse
    {
        private const string BALANCE_KEY = "balance";
        private const string CURRENCY_KEY = "currency";

        public double getBalance ()
        {
            string balance = "0.00";
            contentResponse.TryGetValue(BALANCE_KEY, out balance);
            if (string.IsNullOrEmpty(balance))
            {
                balance = "0.00";
            }
            return double.Parse(balance);
        }

        public string getCurrency()
        {
            string currency = "";
            contentResponse.TryGetValue(CURRENCY_KEY, out currency);
            return currency;
        }
    }
}
