namespace Lipisha.Response
{
    public class AccountFloat : BaseResponse
    {
        private const string FLOAT_KEY = "float";
        private const string ACCOUNT_NUMBER_KEY = "account_number";
        private const string CURRENCY_KEY = "currency";

        public float getAccountFloat ()
        {
            string accountFloat = "0.00";
            contentResponse.TryGetValue(FLOAT_KEY, out accountFloat);
            if (string.IsNullOrEmpty(accountFloat))
            {
                accountFloat = "0.00";
            }
            return float.Parse(accountFloat);
        }

        public string getCurrency ()
        {
            string currency = "";
            contentResponse.TryGetValue(CURRENCY_KEY, out currency);
            return currency;
        }

        public string getAccountNumber()
        {
            string accountNumber = "";
            contentResponse.TryGetValue(ACCOUNT_NUMBER_KEY, out accountNumber);
            return accountNumber;
        }
    }
}
