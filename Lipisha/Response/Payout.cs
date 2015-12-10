namespace Lipisha.Response
{
    public class Payout : BaseResponse
    {
        private const string MOBILE_NUMBER_KEY = "mobile_number";
        private const string AMOUNT_KEY = "amount";
        private const string REFERENCE_KEY = "reference";
        private const string CUSTOMER_NAME_KEY = "customer_name";

        public string getMobileNumber()
        {
            string mobileNumber = "";
            contentResponse.TryGetValue(MOBILE_NUMBER_KEY, out mobileNumber);
            return mobileNumber;
        }

        public double getAmount()
        {
            string amount = "0.00";
            contentResponse.TryGetValue(AMOUNT_KEY, out amount);
            if (string.IsNullOrEmpty(amount)) {
                amount = "0.00";
            }
            return double.Parse(amount);
        }

        public string getReference()
        {
            string reference = "";
            contentResponse.TryGetValue(REFERENCE_KEY, out reference);
            return reference;
        }

        public string getCustomerName()
        {
            string customerName = "";
            contentResponse.TryGetValue(CUSTOMER_NAME_KEY, out customerName);
            return customerName;
        }
    }
}
