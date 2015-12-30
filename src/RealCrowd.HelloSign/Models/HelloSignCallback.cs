using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    public class HelloSignCallback
    {
        [JsonProperty("event")]
        public HelloSignCallbackEvent Event { get; set; }
    }

    public class HelloSignSignatureRequestCallback : HelloSignCallback
    {
        [JsonProperty("signature_request")]
        public SignatureRequest SignatureRequest { get; set; }
    }

    public class HelloSignTemplateCallback : HelloSignCallback
    {
        [JsonProperty("template")]
        public Template Template { get; set; }
    }

    public class HelloSignAccountCallback : HelloSignCallback
    {
        [JsonProperty("account")]
        public Account Account { get; set; }
    }

    public class HelloSignCallbackEvent
    {
        [JsonProperty("event_time")]
        public string EventTime { get; set; }
        [JsonProperty("event_type")]
        public string EventType { get; set; }
        [JsonProperty("event_hash")]
        public string EventHash { get; set; }
        [JsonProperty("event_metadata")]
        public HelloSignCallbackEventMetadata EventMetadata { get; set; }
        [JsonProperty("account_guid")]
        public string AccountGuid { get; set; }
    }

    public class HelloSignCallbackEventMetadata
    {
        [JsonProperty("reported_for_account_id")]
        public string ReportedForAccountId { get; set; }
    }
}
