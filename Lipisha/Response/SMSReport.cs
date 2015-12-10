namespace Lipisha.Response
{
    public class SMSReport : BaseResponse
    {
        private const string RECIPIENT_KEY = "recipient";
        private const string COST_KEY = "cost";
        private const string MESSAGE_KEY = "message";

        public string getMessage ()
        {
            string message = "";
            contentResponse.TryGetValue(MESSAGE_KEY, out message);
            return message;
        }

        public string getRecipient ()
        {
            string recipient = "";
            contentResponse.TryGetValue(RECIPIENT_KEY, out recipient);
            return recipient;
        }

        public double getCost()
        {
            string cost = "0.00";
            contentResponse.TryGetValue(COST_KEY, out cost);
            if (string.IsNullOrEmpty(cost)) {
                cost = "0.00";
            }
            return double.Parse(cost);
        }
    }
}
