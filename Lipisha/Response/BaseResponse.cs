using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Lipisha.Response
{
    public class BaseResponse : BaseStatusResponse
    {
        [JsonProperty("content")]
        public Dictionary<string, string> contentResponse { get; set; }

        public string getResponseValue(string responseKey, string defaultValue=null)
        {
            string responseValue = "";
            contentResponse.TryGetValue(responseKey, out responseValue);
            if (responseValue == null) {
                responseValue = defaultValue;
            }
            return responseValue;
        }

    }
}
