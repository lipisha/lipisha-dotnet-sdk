using System;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class Customer
    {
        [JsonProperty("customer_name")]
        public string customerName { get; set; }
        [JsonProperty("customer_email")]
        public string customerEmail { get; set; }
        [JsonProperty("customer_average")]
        public double customerAverage { get; set; }
        [JsonProperty("customer_first_payment_date")]
        public DateTime firstPaymentDate { get; set; }
        [JsonProperty("customer_last_payment_date")]
        public DateTime lastPaymentDate { get; set; }
        [JsonProperty("customer_mobile_number")]
        public string mobileNumber { get; set; }
        [JsonProperty("customer_payments")]
        public int paymentsCount { get; set; }
        [JsonProperty("customer_total")]
        public double paymentsTotal { get; set; }
    }
}
