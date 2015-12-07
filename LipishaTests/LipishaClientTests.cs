using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lipisha;
using Lipisha.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipisha.Tests
{
    [TestClass()]
    public class LipishaClientTests
    {

        private LipishaClient client;

        [TestInitialize()]
        public void Initialize()
        {
            client = new LipishaClient(TestConfig.TEST_API_KEY,
                TestConfig.TEST_API_SIGNATURE, TestConfig.TEST_ENVIRONMENT);
        }

        [TestMethod()]
        public void getBalanceTest()
        {
            AccountBalance balance = client.getBalance();
            Assert.IsNotNull(balance.contentResponse);
            Assert.IsNotNull(balance.statusResponse);
            Assert.IsNotNull(balance.getBalance());
            Assert.IsNotNull(balance.getCurrency());
            Assert.IsTrue(balance.isSuccessful());
            Console.WriteLine(balance.contentResponse.ToString());
        }

        [TestMethod()]
        public void sendMoneyTest()
        {
            if (string.IsNullOrEmpty(TestConfig.TEST_MOBILE_NUMBER) ||
                string.IsNullOrEmpty(TestConfig.FLOAT_ACCOUNT) ||
                (TestConfig.PAYOUT_AMOUNT < 0.0))
            {
                return;
            }
            Payout payout = client.sendMoney(TestConfig.PAYOUT_ACCOUNT, TestConfig.TEST_MOBILE_NUMBER,
                TestConfig.PAYOUT_AMOUNT);
            Assert.IsNotNull(payout.contentResponse);
            Assert.IsTrue(payout.isSuccessful());
            Assert.AreEqual(TestConfig.TEST_MOBILE_NUMBER, payout.getMobileNumber());
            Assert.IsNotNull(payout.getAmount());
        }

        [TestMethod()]
        public void getFloatTest()
        {
            if (string.IsNullOrEmpty(TestConfig.FLOAT_ACCOUNT))
            {
                return;
            }
            AccountFloat accountFloat = client.getFloat(TestConfig.FLOAT_ACCOUNT);
            Assert.IsNotNull(accountFloat.contentResponse);
            Assert.AreEqual(TestConfig.FLOAT_ACCOUNT, accountFloat.getAccountNumber());
            Assert.IsNotNull(accountFloat.getCurrency());
            Assert.IsNotNull(accountFloat.getAccountFloat());
            Console.WriteLine(accountFloat.contentResponse.ToString());
        }

        [TestMethod()]
        public void sendSmsTest()
        {
            if (string.IsNullOrEmpty(TestConfig.TEST_MOBILE_NUMBER) ||
                string.IsNullOrEmpty(TestConfig.TEST_SMS_MESSAGE))
            {
                return;
            }
            SMSReport report = client.sendSms(TestConfig.TEST_MOBILE_NUMBER, TestConfig.TEST_SMS_MESSAGE);
            Assert.IsNotNull(report.contentResponse);
            Assert.IsTrue(report.isSuccessful());
            Assert.AreEqual(TestConfig.TEST_SMS_MESSAGE, report.getMessage());
            Assert.AreEqual(TestConfig.TEST_MOBILE_NUMBER, report.getRecipient());
            Assert.IsNotNull(report.getCost());
        }

        [TestMethod()]
        public void acknowledgeTransactionTest()
        {
            if (string.IsNullOrEmpty(TestConfig.CONFIRM_TX_ID))
            {
                return;
            }
            TransactionResponse response = client.acknowledgeTransaction(TestConfig.CONFIRM_TX_ID);
            Assert.IsNotNull(response.transaction);
            Assert.IsTrue(response.isSuccessful());
            Assert.AreEqual(TestConfig.CONFIRM_TX_ID, response.transaction.transactionId);
            Assert.AreEqual(TestConfig.STATUS_ACKNOWLEDGED, response.transaction.transactionStatus);
            Assert.IsNotNull(response.transaction.transactionMethod);
            Assert.IsNotNull(response.transaction.transactionDate);
            Assert.IsNotNull(response.transaction.transactionAmount);
        }

        [TestMethod()]
        public void reverseTransactionTest()
        {
            if (string.IsNullOrEmpty(TestConfig.REVERSE_TX_ID))
            {
                return;
            }
            MultiTransactionResponse response = client.reverseTransaction(TestConfig.REVERSE_TX_ID);
            Assert.IsNotNull(response.transactions);
            Assert.IsTrue(response.isSuccessful());
            Assert.AreEqual(TestConfig.STATUS_REVERSAL_REQUESTED, response.getStatus());
        }

        [TestMethod()]
        public void sendAirtimeTest()
        {
            if (string.IsNullOrEmpty(TestConfig.TEST_MOBILE_NUMBER) ||
                string.IsNullOrEmpty(TestConfig.AIRIME_NETWORK) ||
                string.IsNullOrEmpty(TestConfig.AIRTIME_ACCOUNT) ||
                (TestConfig.AIRTIME_AMOUNT <= 0))
            {
                return;
            }
            AirtimeDisbursement disbursement = client.sendAirtime(TestConfig.AIRTIME_ACCOUNT,
                TestConfig.TEST_MOBILE_NUMBER, TestConfig.AIRTIME_AMOUNT, TestConfig.AIRIME_NETWORK);
            Assert.IsNotNull(disbursement.contentResponse);
            Assert.IsTrue(disbursement.isSuccessful());
            Assert.AreEqual(TestConfig.TEST_MOBILE_NUMBER, disbursement.getMobileNumber());
            Assert.AreEqual(TestConfig.AIRTIME_AMOUNT, disbursement.getAmount());
            Assert.IsNotNull(disbursement.getReference());
        }

        [TestMethod()]
        public void createUserTest()
        {
            if (string.IsNullOrEmpty(TestConfig.TEST_USER_EMAIL) ||
                string.IsNullOrEmpty(TestConfig.TEST_USER_LOGIN) ||
                string.IsNullOrEmpty(TestConfig.TEST_USER_PASSWORD) ||
                string.IsNullOrEmpty(TestConfig.TEST_USER_MOBILE) ||
                string.IsNullOrEmpty(TestConfig.TEST_USER_NAMES) ||
                string.IsNullOrEmpty(TestConfig.TEST_USER_ROLE))
            {
                return;
            }
            UserResponse response = client.createUser(TestConfig.TEST_USER_NAMES,
                TestConfig.TEST_USER_ROLE,
                TestConfig.TEST_USER_MOBILE,
                TestConfig.TEST_USER_EMAIL,
                TestConfig.TEST_USER_LOGIN,
                TestConfig.TEST_USER_PASSWORD);
            Assert.IsNotNull(response.user);
            Assert.IsTrue(response.isSuccessful());
            Assert.AreEqual(TestConfig.TEST_USER_LOGIN, response.user.userName);
            Assert.AreEqual(TestConfig.TEST_USER_EMAIL, response.user.email);
        }

        [TestMethod()]
        public void createPaymentAccountTest()
        {
            if ((TestConfig.TRANSACTION_ACCOUNT_TYPE <= 0) ||
                string.IsNullOrEmpty(TestConfig.TRANSACTION_ACCOUNT_MANAGER) ||
                string.IsNullOrEmpty(TestConfig.TRANSACTION_ACCOUNT_NAME))
            {
                return;
            }
            TransactionAccountResponse response = client.createPaymentAccount(
                TestConfig.TRANSACTION_ACCOUNT_TYPE,
                TestConfig.TRANSACTION_ACCOUNT_NAME,
                TestConfig.TRANSACTION_ACCOUNT_MANAGER);
            Assert.IsNotNull(response.transactionAccount);
            Assert.IsTrue(response.isSuccessful());
            Assert.AreEqual(TestConfig.TRANSACTION_ACCOUNT_NAME, response.transactionAccount.accountName);
            Assert.AreEqual(TestConfig.TRANSACTION_ACCOUNT_TYPE, response.transactionAccount.accountType);
            Assert.IsNotNull(response.transactionAccount.accountNumber);
        }

        [TestMethod()]
        public void createWithdrawalAccountTest()
        {
            if ((TestConfig.WITHDRAWAL_ACCOUNT_TYPE <= 0) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_NAME) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_NUMBER) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_BANK_NAME) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_BANK_BRANCH) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_BANK_ADDRESS) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_SWIFT_CODE) ||
            string.IsNullOrEmpty(TestConfig.WITHDRAWAL_ACCOUNT_MANAGER))
            {
                return;
            }

            WithdrawalAccountResponse response = client.createWithdrawalAccount(
                TestConfig.WITHDRAWAL_ACCOUNT_TYPE,
                TestConfig.WITHDRAWAL_ACCOUNT_NAME,
                TestConfig.WITHDRAWAL_ACCOUNT_NUMBER,
                TestConfig.WITHDRAWAL_ACCOUNT_BANK_NAME,
                TestConfig.WITHDRAWAL_ACCOUNT_BANK_BRANCH,
                TestConfig.WITHDRAWAL_ACCOUNT_BANK_ADDRESS,
                TestConfig.WITHDRAWAL_ACCOUNT_SWIFT_CODE,
                TestConfig.WITHDRAWAL_ACCOUNT_MANAGER);
            Assert.IsNotNull(response.withdrawalAccount);
            Assert.IsTrue(response.isSuccessful());
            Assert.AreEqual(TestConfig.WITHDRAWAL_ACCOUNT_BANK_NAME, response.withdrawalAccount.bankName);
            Assert.AreEqual(TestConfig.WITHDRAWAL_ACCOUNT_BANK_BRANCH, response.withdrawalAccount.bankBranch);
            Assert.AreEqual(TestConfig.WITHDRAWAL_ACCOUNT_BANK_ADDRESS, response.withdrawalAccount.bankAddress);
            Assert.AreEqual(TestConfig.WITHDRAWAL_ACCOUNT_SWIFT_CODE, response.withdrawalAccount.swiftCode);
        }

        [TestMethod()]
        public void getTransactionsTest()
        {
            MultiTransactionResponse response = client.getTransactions();
            Assert.IsNotNull(response.transactions);
            Assert.IsTrue(response.isSuccessful());
            if (response.transactions.Count() > 0)
            {
                Transaction transaction = response.transactions[0];
                Assert.IsNotNull(transaction.transactionId);
                Assert.IsNotNull(transaction.transactionDate);
                Assert.IsNotNull(transaction.transactionAmount);
                Assert.IsNotNull(transaction.transactionMethod);
            }
        }

        [TestMethod()]
        public void getTransactionsByIdTest()
        {
            if (string.IsNullOrEmpty(TestConfig.TRANSACTION_SEARCH_ID))
            {
                return;
            }
            MultiTransactionResponse response = client.getTransactions(TestConfig.TRANSACTION_SEARCH_ID);
            Assert.IsNotNull(response.transactions);
            Assert.IsTrue(response.isSuccessful());
            Assert.AreNotEqual(0, response.transactions.Count());
            Transaction transaction = response.transactions[0];
            Assert.AreEqual(TestConfig.TRANSACTION_SEARCH_ID, transaction.transactionId);
        }

        [TestMethod()]
        public void getTransactionsByDateTest()
        {
            if (string.IsNullOrEmpty(TestConfig.TRANSACTION_SEARCH_DATE_START) ||
                string.IsNullOrEmpty(TestConfig.TRANSACTION_SEARCH_DATE_END))
            {
                return;
            }
            MultiTransactionResponse response = client.getTransactions("", "", "",
                TestConfig.TRANSACTION_SEARCH_DATE_START, TestConfig.TRANSACTION_SEARCH_DATE_END,
                "", "", "", 0.0, 100000000.0, "", "", "", "", 10, 0);
            Assert.IsNotNull(response.transactions);
            Assert.IsTrue(response.isSuccessful());
            if (response.transactions.Count() > 0)
            {
                Transaction transaction = response.transactions[0];
                Assert.IsNotNull(transaction.transactionId);
                Assert.IsNotNull(transaction.transactionDate);
                Assert.IsNotNull(transaction.transactionMethod);
                Assert.IsNotNull(transaction.transactionAmount);

            }
        }

        [TestMethod()]
        public void getCustomersTest()
        {
            CustomerResponse response = client.getCustomers();
            Assert.IsNotNull(response.customers);
            Assert.IsTrue(response.isSuccessful());
            if (response.customers.Count() > 0)
            {
                Customer customer = response.customers[0];
                Assert.IsNotNull(customer.customerName);
                Assert.IsNotNull(customer.mobileNumber);
                Assert.IsNotNull(customer.customerAverage);
                Assert.IsNotNull(customer.paymentsTotal);
            }
        }

        public bool chargeCard()
        {
            return !(string.IsNullOrEmpty(TestConfig.CARD_ACCOUNT) ||
                string.IsNullOrEmpty(TestConfig.CARD_NUMBER) ||
                string.IsNullOrEmpty(TestConfig.CARD_ADDRESS_1) ||
                string.IsNullOrEmpty(TestConfig.CARD_ADDRESS_2) ||
                string.IsNullOrEmpty(TestConfig.CARD_EXPIRY) ||
                string.IsNullOrEmpty(TestConfig.CARD_NAME) ||
                string.IsNullOrEmpty(TestConfig.CARD_COUNTRY) ||
                string.IsNullOrEmpty(TestConfig.CARD_STATE) ||
                string.IsNullOrEmpty(TestConfig.CARD_ZIP) ||
                string.IsNullOrEmpty(TestConfig.CARD_SECURITY_CODE) ||
                (TestConfig.CARD_AMOUNT <= 0) ||
                string.IsNullOrEmpty(TestConfig.CARD_CURRENCY));
        }

        [TestMethod()]
        public void authorizeCardTransactionTest()
        {

            if (!chargeCard())
            {
                return;
            }

            CardTransactionResponse response = client.authorizeCardTransaction(
                TestConfig.CARD_ACCOUNT,
                TestConfig.CARD_NUMBER,
                TestConfig.CARD_ADDRESS_1,
                TestConfig.CARD_ADDRESS_2,
                TestConfig.CARD_EXPIRY,
                TestConfig.CARD_NAME,
                TestConfig.CARD_COUNTRY,
                TestConfig.CARD_STATE,
                TestConfig.CARD_ZIP,
                TestConfig.CARD_SECURITY_CODE,
                TestConfig.CARD_AMOUNT,
                TestConfig.CARD_CURRENCY);
            Assert.IsNotNull(response.contentResponse);
            Assert.IsTrue(response.isSuccessful());
            Assert.IsNotNull(response.getTransactionIndex());
            Assert.IsNotNull(response.getTransactionReference());
        }

        [TestMethod()]
        public void reverseCardTransactionTest()
        {
            if (!chargeCard())
            {
                return;
            }
            CardTransactionResponse authorization = client.authorizeCardTransaction(
                TestConfig.CARD_ACCOUNT,
                TestConfig.CARD_NUMBER,
                TestConfig.CARD_ADDRESS_1,
                TestConfig.CARD_ADDRESS_2,
                TestConfig.CARD_EXPIRY,
                TestConfig.CARD_NAME,
                TestConfig.CARD_COUNTRY,
                TestConfig.CARD_STATE,
                TestConfig.CARD_ZIP,
                TestConfig.CARD_SECURITY_CODE,
                TestConfig.CARD_AMOUNT,
                TestConfig.CARD_CURRENCY);
            Assert.IsNotNull(authorization.contentResponse);
            Assert.IsTrue(authorization.isSuccessful());
            Assert.IsNotNull(authorization.getTransactionIndex());
            Assert.IsNotNull(authorization.getTransactionReference());

            CardTransactionResponse reversal = client.reverseCardTransaction(
                authorization.getTransactionIndex(), authorization.getTransactionReference());
            Assert.IsNotNull(reversal.contentResponse);
            Assert.IsTrue(reversal.isSuccessful());
            Assert.IsNotNull(reversal.getTransactionIndex());
            Assert.IsNotNull(reversal.getTransactionReference());

        }

        [TestMethod()]
        public void completeCardTransactionTest()
        {
            if (!chargeCard())
            {
                return;
            }
            CardTransactionResponse authorization = client.authorizeCardTransaction(
                TestConfig.CARD_ACCOUNT,
                TestConfig.CARD_NUMBER,
                TestConfig.CARD_ADDRESS_1,
                TestConfig.CARD_ADDRESS_2,
                TestConfig.CARD_EXPIRY,
                TestConfig.CARD_NAME,
                TestConfig.CARD_COUNTRY,
                TestConfig.CARD_STATE,
                TestConfig.CARD_ZIP,
                TestConfig.CARD_SECURITY_CODE,
                TestConfig.CARD_AMOUNT,
                TestConfig.CARD_CURRENCY);
            Assert.IsNotNull(authorization.contentResponse);
            Assert.IsTrue(authorization.isSuccessful());
            Assert.IsNotNull(authorization.getTransactionIndex());
            Assert.IsNotNull(authorization.getTransactionReference());

            CardTransactionResponse completeCardTransaction = client.reverseCardTransaction(
                authorization.getTransactionIndex(), authorization.getTransactionReference());
            Assert.IsNotNull(completeCardTransaction.contentResponse);
            Assert.IsTrue(completeCardTransaction.isSuccessful());
            Assert.IsNotNull(completeCardTransaction.getTransactionIndex());
            Assert.IsNotNull(completeCardTransaction.getTransactionReference());

        }

    }
}