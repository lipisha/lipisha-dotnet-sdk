namespace Lipisha.Response
{
    public class AirtimeDisbursement: BaseResponse
    {
        private const string MOBILE_NUMBER_KEY = "mobile_number";
        private const string AMOUNT_KEY = "amount";
        private const string REFERENCE_KEY = "reference";

        public string getMobileNumber ()
        {
            return getResponseValue(MOBILE_NUMBER_KEY);
        }

        public double getAmount()
        {
            string amount = getResponseValue(AMOUNT_KEY, "0.00");
            return double.Parse(amount);
        }

        public string getReference()
        {
            return getResponseValue(REFERENCE_KEY);
        }
    }
}
