using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace LipishaIPNMVC.Controllers
{

	public class LipishaData
	{
		public string api_key { get; set; }
		public string api_signature { get; set; }
		public string api_type { get; set; }
		public string transaction_reference { get; set; }
		public string transaction_status { get; set; }
		public string transaction_status_code { get; set; }
		public string transaction_status_description { get; set; }

		public bool authenticate(string apiKey, string apiSignature)
		{
			return api_key == apiKey && apiSignature == api_signature;
		}
	}
		
	public class HomeController : Controller
	{
		private const string API_KEY = "<YOUR API KEY>";
		private const string API_SIGNATURE = "<YOUR API SIGNATURE>";
		private const string API_VERSION = "1.3.0";
		private const string ACTION_INITIATE = "Initiate";
		private const string ACTION_ACKNOWLEDGE = "Acknowledge";
		private const string ACTION_RECEIPT = "Receipt";
		private const string STATUS_SUCCESS = "Success";
		private const string STATUS_SUCCESS_CODE = "001";

		[HttpPost]
		public ActionResult Index (LipishaData lipishaData)
		{
			Dictionary<string, string> response = new Dictionary<string, string>();
			if (lipishaData.authenticate (API_KEY, API_SIGNATURE)) {
				if (lipishaData.api_type == ACTION_INITIATE) {
					// Respond to Lipisha confirming receipt of payment IPN push
					// We can store the transaction in draft state awaiting acknowledgement
					response.Add ("api_key", API_KEY);
					response.Add ("api_signature", API_SIGNATURE);
					response.Add ("api_type", ACTION_RECEIPT);
					response.Add ("transaction_reference", lipishaData.transaction_reference);
					response.Add ("transaction_status_code", STATUS_SUCCESS_CODE);
					response.Add ("transaction_status", STATUS_SUCCESS);
					response.Add ("transaction_status_description", "Transaction Received");
					response.Add ("transaction_custom_sms", "Payment Received. Thank you.");
				} else if (lipishaData.api_type == ACTION_ACKNOWLEDGE) {
					// Lipisha will then send an acknowledgement of this confirmation
					// At this point we can update the transaction received in the initiate action
					// above.
					Console.WriteLine ("Transaction: Reference: " + lipishaData.transaction_reference);
					Console.WriteLine ("Transaction: Status: " + lipishaData.transaction_status);
					response.Add ("status", "OK");
				} else {
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Unknown Request");
				}
			} else {
				return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Bad Unauthorized");
			}
				
			return Json (response);
		}
			
	}
}

