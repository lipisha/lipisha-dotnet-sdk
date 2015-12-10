using System;

namespace Lipisha.Response
{
    public class CardTransactionResponse : BaseResponse
    {
        private const string TRANSACTION_INDEX_KEY = "transaction_index";
        private const string TRANSACTION_REFERENCE_KEY = "transaction_reference";

        public String getTransactionIndex ()
        {
            return getResponseValue(TRANSACTION_INDEX_KEY);
        }

        public String getTransactionReference()
        {
            return getResponseValue(TRANSACTION_REFERENCE_KEY);
        }
    }
}
