using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Lipisha.Response;

namespace Lipisha
{

    public class LipishaResponse
    {

        public string rawResponseText;

        public LipishaResponse(string responseString)
        {
            rawResponseText = responseString;
        }

    }

    public class LipishaClient
    {

        // Constants
        private const string API_VERSION = "1.3.0";
        private const string API_TYPE_DEFAULT = "Callback";
        private const string ENV_PROD = "LIVE";
        private const string ENV_TEST = "TEST";
        private const string LIVE_URL = "https://lipisha.com/payments/accounts/index.php/v2/api/";
        private const string SANDBOX_URL = "http://developer.lipisha.com/index.php/v2/api/";


        // Serialization/Deserialization Settings
        private JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };

        //Search Defaults
        private const double MIN_AMOUNT = 0.0;
        private const double MAX_AMOUNT = 1000000.0;

        // Configurable
        private string apiKey;
        private string apiSignature;
        private string apiType;
        private string apiBaseUrl;

        public LipishaClient(string apiKey, string apiSignature, string environment, string apiType = API_TYPE_DEFAULT)
        {
            switch (environment.ToUpper())
            {
                case ENV_PROD:
                    this.apiBaseUrl = LIVE_URL;
                    break;
                case ENV_TEST:
                    this.apiBaseUrl = SANDBOX_URL;
                    break;
                default:
                    throw new Exception("environment must be either `live` or `test`");
            }

            this.apiType = apiType;
            this.apiKey = apiKey;
            this.apiSignature = apiSignature;
        }

        public string execute(string endpoint, Dictionary<string, string> parameters)
        {
            // Populate default parameters
            parameters.Add("api_type", this.apiType);
            parameters.Add("api_key", this.apiKey);
            parameters.Add("api_signature", this.apiSignature);
            string endpointUrl = apiBaseUrl + endpoint;
            WebClient client = new WebClient();
            NameValueCollection values = new NameValueCollection();
            foreach (var param in parameters)
            {
                values.Add(param.Key, param.Value);
            }
            byte[] response = client.UploadValues(endpointUrl, values);
            string result = System.Text.Encoding.UTF8.GetString(response);
            return result;
        }



        /// <summary>Get balance.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_get_balance">get_balance</see>
        /// </summary>
        /// <returns>AccountBalance instance</returns>
        public AccountBalance getBalance()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string response = execute("get_balance", parameters);
            return JsonConvert.DeserializeObject<AccountBalance>(response);
        }


        /// <summary>Send money.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_send_money">send_money</see>
        /// </summary>
        /// <param name="account_number">Account number</param>
        /// <param name="mobile_number">Mobile number</param>
        /// <param name="amount">Amount</param>
        /// <returns>Payout instance</returns>
        public Payout sendMoney(string account_number, string mobile_number, double amount)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("account_number", account_number);
            parameters.Add("mobile_number", mobile_number);
            parameters.Add("amount", amount.ToString());
            string response = execute("send_money", parameters);
            return JsonConvert.DeserializeObject<Payout>(response);
        }


        /// <summary>Get float.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_get_float">get_float</see>
        /// </summary>
        /// <param name="account_number">Account number</param>
        /// <returns>AccountFloat instance</returns>
        public AccountFloat getFloat(string account_number)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("account_number", account_number);
            string response = execute("get_float", parameters);
            return JsonConvert.DeserializeObject<AccountFloat>(response);
        }

        /// <summary>Send sms.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_send_sms">send_sms</see>
        /// </summary>
        /// <param name="mobile_number">Mobile number</param>
        /// <param name="message">Message</param>
        /// <returns>SMSReport instance</returns>
        public SMSReport sendSms(string mobile_number, string message)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("mobile_number", mobile_number);
            parameters.Add("message", message);
            string response = execute("send_sms", parameters);
            return JsonConvert.DeserializeObject<SMSReport>(response);
        }


        /// <summary>Acknowledge transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_acknowledge_transaction">acknowledge_transaction</see>
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns>TransactionResponse instance</returns>
        public TransactionResponse acknowledgeTransaction(string transaction)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction", transaction);
            string response = execute("acknowledge_transaction", parameters);
            return JsonConvert.DeserializeObject<TransactionResponse>(response, serializerSettings);
        }

        /// <summary>Confirm transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_confirm_transaction">confirm_transaction</see>
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns>LipishaResponse instance</returns>
        public string confirmTransaction(string transaction)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction", transaction);
            return execute("confirm_transaction", parameters);
        }


        /// <summary>Reverse transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_reverse_transaction">reverse_transaction</see>
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <returns>MultiTransactionResponse instance</returns>
        public MultiTransactionResponse reverseTransaction(string transaction)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction", transaction);
            string response = execute("reverse_transaction", parameters);
            return JsonConvert.DeserializeObject<MultiTransactionResponse>(response, serializerSettings);
        }


        /// <summary>Send airtime.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_send_airtime">send_airtime</see>
        /// </summary>
        /// <param name="account_number">Account number</param>
        /// <param name="mobile_number">Mobile number</param>
        /// <param name="amount">Amount</param>
        /// <param name="network">Network</param>
        /// <returns>AirtimeDisbursement instance</returns>
        public AirtimeDisbursement sendAirtime(string account_number, string mobile_number, double amount, string network)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("account_number", account_number);
            parameters.Add("mobile_number", mobile_number);
            parameters.Add("amount", amount.ToString());
            parameters.Add("network", network);
            string response = execute("send_airtime", parameters);
            return JsonConvert.DeserializeObject<AirtimeDisbursement>(response);
        }


        /// <summary>Create user.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_create_user">create_user</see>
        /// </summary>
        /// <param name="full_name">Full name</param>
        /// <param name="role">Role</param>
        /// <param name="mobile_number">Mobile number</param>
        /// <param name="email">Email</param>
        /// <param name="user_name">User name</param>
        /// <param name="password">Password</param>
        /// <returns>LipishaResponse instance</returns>
        public UserResponse createUser(string full_name, string role, string mobile_number, string email, string user_name, string password)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("full_name", full_name);
            parameters.Add("role", role);
            parameters.Add("mobile_number", mobile_number);
            parameters.Add("email", email);
            parameters.Add("user_name", user_name);
            parameters.Add("password", password);
            string response = execute("create_user", parameters);
            return JsonConvert.DeserializeObject<UserResponse>(response);
        }



        /// <summary>Create payment account.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_create_payment_account">create_payment_account</see>
        /// </summary>
        /// <param name="transaction_account_type">Transaction account type</param>
        /// <param name="transaction_account_name">Transaction account name</param>
        /// <param name="transaction_account_manager">Transaction account manager</param>
        /// <returns>TransactionAccountResponse instance</returns>
        public TransactionAccountResponse createPaymentAccount(int transaction_account_type, string transaction_account_name, string transaction_account_manager)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction_account_type", transaction_account_type.ToString());
            parameters.Add("transaction_account_name", transaction_account_name);
            parameters.Add("transaction_account_manager", transaction_account_manager);
            string response = execute("create_payment_account", parameters);
            return JsonConvert.DeserializeObject<TransactionAccountResponse>(response);
        }


        /// <summary>Create withdrawal account.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_create_withdrawal_account">create_withdrawal_account</see>
        /// </summary>
        /// <param name="transaction_account_type">Transaction account type</param>
        /// <param name="transaction_account_name">Transaction account name</param>
        /// <param name="transaction_account_number">Transaction account number</param>
        /// <param name="transaction_account_bank_name">Transaction account bank name</param>
        /// <param name="transaction_account_bank_branch">Transaction account bank branch</param>
        /// <param name="transaction_account_bank_address">Transaction account bank address</param>
        /// <param name="transaction_account_swift_code">Transaction account swift code</param>
        /// <param name="transaction_account_manager">Transaction account manager</param>
        /// <returns>WithdrawalAccountResponse instance</returns>
        public WithdrawalAccountResponse createWithdrawalAccount(int transaction_account_type, string transaction_account_name, string transaction_account_number, string transaction_account_bank_name, string transaction_account_bank_branch, string transaction_account_bank_address, string transaction_account_swift_code, string transaction_account_manager)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction_account_type", transaction_account_type.ToString());
            parameters.Add("transaction_account_name", transaction_account_name);
            parameters.Add("transaction_account_number", transaction_account_number);
            parameters.Add("transaction_account_bank_name", transaction_account_bank_name);
            parameters.Add("transaction_account_bank_branch", transaction_account_bank_branch);
            parameters.Add("transaction_account_bank_address", transaction_account_bank_address);
            parameters.Add("transaction_account_swift_code", transaction_account_swift_code);
            parameters.Add("transaction_account_manager", transaction_account_manager);
            string response = execute("create_withdrawal_account", parameters);
            return JsonConvert.DeserializeObject<WithdrawalAccountResponse>(response);
        }


        /// <summary>Get transactions.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_get_transactions">get_transactions</see>
        /// </summary>
        /// <param name="transaction">Transaction</param>
        /// <param name="transaction_type">Transaction type</param>
        /// <param name="transaction_method">Transaction method</param>
        /// <param name="transaction_date_start">Transaction date start: Format yyyy-mm-dd hh:mm:s</param>
        /// <param name="transaction_date_end">Transaction date end. Format yyyy-mm-dd hh:mm:ss</param>
        /// <param name="transaction_account_name">Transaction account name</param>
        /// <param name="transaction_account_number">Transaction account number</param>
        /// <param name="transaction_reference">Transaction reference</param>
        /// <param name="transaction_amount_minimum">Transaction amount minimum</param>
        /// <param name="transaction_amount_maximum">Transaction amount maximum</param>
        /// <param name="transaction_status">Transaction status</param>
        /// <param name="transaction_name">Transaction name</param>
        /// <param name="transaction_mobile_number">Transaction mobile number</param>
        /// <param name="transaction_email">Transaction email</param>
        /// <param name="limit">Limit</param>
        /// <param name="offset">Offset</param>
        /// <returns>MultiTransactionResponse instance</returns>
        public MultiTransactionResponse getTransactions(string transaction = "",
            string transaction_type = "",
            string transaction_method = "",
            string transaction_date_start = "",
            string transaction_date_end = "",
            string transaction_account_name = "",
            string transaction_account_number = "",
            string transaction_reference = "",
            double transaction_amount_minimum = MIN_AMOUNT,
            double transaction_amount_maximum = MAX_AMOUNT, 
            string transaction_status = "",
            string transaction_name = "",
            string transaction_mobile_number = "",
            string transaction_email = "",
            int limit = 1000,
            int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction", transaction);
            parameters.Add("transaction_type", transaction_type);
            parameters.Add("transaction_method", transaction_method);
            parameters.Add("transaction_date_start", transaction_date_start);
            parameters.Add("transaction_date_end", transaction_date_end);
            parameters.Add("transaction_account_name", transaction_account_name);
            parameters.Add("transaction_account_number", transaction_account_number);
            parameters.Add("transaction_reference", transaction_reference);
            parameters.Add("transaction_amount_minimum", transaction_amount_minimum.ToString());
            parameters.Add("transaction_amount_maximum", transaction_amount_maximum.ToString());
            parameters.Add("transaction_status", transaction_status);
            parameters.Add("transaction_name", transaction_name);
            parameters.Add("transaction_mobile_number", transaction_mobile_number);
            parameters.Add("transaction_email", transaction_email);
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            string response = execute("get_transactions", parameters);
            return JsonConvert.DeserializeObject<MultiTransactionResponse>(response, serializerSettings);
        }


        /// <summary>Get customers.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_get_customers">get_customers</see>
        /// </summary>
        /// <param name="customer_name">Customer name</param>
        /// <param name="customer_mobile_number">Customer mobile number</param>
        /// <param name="customer_email">Customer email</param>
        /// <param name="customer_first_payment_from">Customer first payment from</param>
        /// <param name="customer_first_payment_to">Customer first payment to</param>
        /// <param name="customer_last_payment_from">Customer last payment from</param>
        /// <param name="customer_last_payment_to">Customer last payment to</param>
        /// <param name="customer_payments_minimum">Customer payments minimum</param>
        /// <param name="customer_payments_maximum">Customer payments maximum</param>
        /// <param name="customer_total_spent_minimum">Customer total spent minimum</param>
        /// <param name="customer_total_spent_maximum">Customer total spent maximum</param>
        /// <param name="customer_average_spent_minimum">Customer average spent minimum</param>
        /// <param name="customer_average_spent_maximum">Customer average spent maximum</param>
        /// <param name="limit">Limit</param>
        /// <param name="offset">Offset</param>
        /// <returns>CustomerResponse instance</returns>
        public CustomerResponse getCustomers(
            string customer_name = "",
            string customer_mobile_number = "",
            string customer_email = "",
            string customer_first_payment_from = "",
            string customer_first_payment_to = "",
            string customer_last_payment_from = "",
            string customer_last_payment_to = "",
            double customer_payments_minimum = MIN_AMOUNT,
            double customer_payments_maximum = MAX_AMOUNT,
            double customer_total_spent_minimum = MIN_AMOUNT,
            double customer_total_spent_maximum = MAX_AMOUNT,
            double customer_average_spent_minimum = MIN_AMOUNT,
            double customer_average_spent_maximum = MAX_AMOUNT,
            int limit = 1000,
            int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("customer_name", customer_name);
            parameters.Add("customer_mobile_number", customer_mobile_number);
            parameters.Add("customer_email", customer_email);
            parameters.Add("customer_first_payment_from", customer_first_payment_from);
            parameters.Add("customer_first_payment_to", customer_first_payment_to);
            parameters.Add("customer_last_payment_from", customer_last_payment_from);
            parameters.Add("customer_last_payment_to", customer_last_payment_to);
            parameters.Add("customer_payments_minimum", customer_payments_minimum.ToString());
            parameters.Add("customer_payments_maximum", customer_payments_maximum.ToString());
            parameters.Add("customer_total_spent_minimum", customer_total_spent_minimum.ToString());
            parameters.Add("customer_total_spent_maximum", customer_total_spent_maximum.ToString());
            parameters.Add("customer_average_spent_minimum", customer_average_spent_minimum.ToString());
            parameters.Add("customer_average_spent_maximum", customer_average_spent_maximum.ToString());
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());
            string response = execute("get_customers", parameters);
            return JsonConvert.DeserializeObject<CustomerResponse>(response);
        }


        /// <summary>Authorize card transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_authorize_card_transaction">authorize_card_transaction</see>
        /// </summary>
        /// <param name="account_number">Account number</param>
        /// <param name="card_number">Card number</param>
        /// <param name="address1">Address1</param>
        /// <param name="address2">Address2</param>
        /// <param name="expiry">Expiry. Format MMYYYY</param>
        /// <param name="name">Name</param>
        /// <param name="country">Country</param>
        /// <param name="state">State</param>
        /// <param name="zip">Zip</param>
        /// <param name="security_code">Security code</param>
        /// <param name="amount">Amount</param>
        /// <param name="currency">Currency e.g. KES or USD</param>
        /// <returns>CardTransactionResponse instance</returns>
        public CardTransactionResponse authorizeCardTransaction(
            string account_number,
            string card_number,
            string address1,
            string address2,
            string expiry,
            string name,
            string country,
            string state,
            string zip,
            string security_code,
            double amount,
            string currency)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("account_number", account_number);
            parameters.Add("card_number", card_number);
            parameters.Add("address1", address1);
            parameters.Add("address2", address2);
            parameters.Add("expiry", expiry);
            parameters.Add("name", name);
            parameters.Add("country", country);
            parameters.Add("state", state);
            parameters.Add("zip", zip);
            parameters.Add("security_code", security_code);
            parameters.Add("amount", amount.ToString());
            parameters.Add("currency", currency);
            string response = execute("authorize_card_transaction", parameters);
            return JsonConvert.DeserializeObject<CardTransactionResponse>(response);
        }


        /// <summary>Reverse card transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_reverse_card_transaction">reverse_card_transaction</see>
        /// </summary>
        /// <param name="transaction_index">Transaction index</param>
        /// <param name="transaction_reference">Transaction reference</param>
        /// <returns>CardTransactionResponse instance</returns>
        public CardTransactionResponse reverseCardTransaction(string transaction_index, string transaction_reference)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction_index", transaction_index);
            parameters.Add("transaction_reference", transaction_reference);
            string response = execute("reverse_card_transaction", parameters);
            return JsonConvert.DeserializeObject<CardTransactionResponse>(response);
        }


        /// <summary>Complete card transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_complete_card_transaction">complete_card_transaction</see>
        /// </summary>
        /// <param name="transaction_index">Transaction index</param>
        /// <param name="transaction_reference">Transaction reference</param>
        /// <returns>CardTransactionResponse instance</returns>
        public CardTransactionResponse completeCardTransaction(string transaction_index, string transaction_reference)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction_index", transaction_index);
            parameters.Add("transaction_reference", transaction_reference);
            string response = execute("complete_card_transaction", parameters);
            return JsonConvert.DeserializeObject<CardTransactionResponse>(response);
        }


        /// <summary>Void card transaction.
        /// See full documentation here: <see href="http://developer.lipisha.com/index.php/app/launch/api_void_card_transaction">void_card_transaction</see>
        /// </summary>
        /// <param name="transaction_index">Transaction index</param>
        /// <param name="transaction_reference">Transaction reference</param>
        /// <returns>CardTransactionResponse instance</returns>
        public CardTransactionResponse voidCardTransaction(string transaction_index, string transaction_reference)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("transaction_index", transaction_index);
            parameters.Add("transaction_reference", transaction_reference);
            string response = execute("void_card_transaction", parameters);
            return JsonConvert.DeserializeObject<CardTransactionResponse>(response);
        }

    }
}
