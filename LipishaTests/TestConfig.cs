using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipisha.Tests
{
    public class TestConfig
    {
        public const string TEST_API_KEY = "";
        public const string TEST_API_SIGNATURE = "";
        public const string TEST_ENVIRONMENT = "";
        public const string FLOAT_ACCOUNT = "";
        public const string PAYOUT_ACCOUNT = "";
        public const string AIRTIME_ACCOUNT = "";
        public const string TEST_MOBILE_NUMBER = "";
        public const double PAYOUT_AMOUNT = 0.00;
        public const string TEST_SMS_MESSAGE = "";

        // Transaction tests config
        public const string STATUS_CONFIRMED = "";
        public const string STATUS_ACKNOWLEDGED = "";
        public const string STATUS_REVERSAL_REQUESTED = "";
        public const string CONFIRM_TX_ID = "";
        public const string REVERSE_TX_ID = "";

        // Send Airime
        public const string AIRIME_NETWORK = "";
        public const double AIRTIME_AMOUNT = 0;

        // User Setup
        public const string TEST_USER_NAMES = "";
        public const string TEST_USER_ROLE = "";
        public const string TEST_USER_EMAIL = "";
        public const string TEST_USER_MOBILE = "";
        public const string TEST_USER_LOGIN = "";
        public const string TEST_USER_PASSWORD = "";

        // Payment Account Parameters
        public const int TRANSACTION_ACCOUNT_TYPE = 1; //Mobile money
        public const string TRANSACTION_ACCOUNT_NAME = "";
        public const string TRANSACTION_ACCOUNT_MANAGER = "";

        // Withdrawal Account Parameters
        public const int WITHDRAWAL_ACCOUNT_TYPE = 1; // Bank account
        public const string WITHDRAWAL_ACCOUNT_NAME = "";
        public const string WITHDRAWAL_ACCOUNT_NUMBER = "";
        public const string WITHDRAWAL_ACCOUNT_BANK_NAME = "";
        public const string WITHDRAWAL_ACCOUNT_BANK_BRANCH = "";
        public const string WITHDRAWAL_ACCOUNT_BANK_ADDRESS = "";
        public const string WITHDRAWAL_ACCOUNT_SWIFT_CODE = "";
        public const string WITHDRAWAL_ACCOUNT_MANAGER = "";

        // Search Transactions
        public const string TRANSACTION_SEARCH_ID = "";
        public const string TRANSACTION_SEARCH_DATE_START = "";
        public const string TRANSACTION_SEARCH_DATE_END = "";


        // Card Settings
        public const string CARD_ACCOUNT = "";
        public const string CARD_NUMBER = "";
        public const string CARD_ADDRESS_1 = "";
        public const string CARD_ADDRESS_2 = "";
        public const string CARD_EXPIRY = "";
        public const string CARD_NAME = "";
        public const string CARD_COUNTRY = "";
        public const string CARD_STATE = "";
        public const string CARD_ZIP = "";
        public const string CARD_SECURITY_CODE = "";
        public const double CARD_AMOUNT = 0.00;
        public const string CARD_CURRENCY = "";
    }
}
