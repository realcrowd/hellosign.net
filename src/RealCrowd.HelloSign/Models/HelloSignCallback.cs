using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    public class HelloSignCallback
    {
        public JObject JsonPayload { get; set; }

        public static HelloSignCallback Parse(string jsonData)
        {
            var cb = new HelloSignCallback();

            cb.JsonPayload = JsonConvert.DeserializeObject<JObject>(jsonData);

            cb.Event = cb.JsonPayload["event"].ToObject<HelloSignCallbackEvent>();

            if (cb.ExpectedAttachedModelType == typeof(SignatureRequest))
            {
                cb.AttachedModel = cb.JsonPayload["signature_request"].ToObject<SignatureRequest>();
            }
            else if (cb.ExpectedAttachedModelType == typeof(Account))
            {
                cb.AttachedModel = cb.JsonPayload["account"].ToObject<Account>();
            }
            else if (cb.ExpectedAttachedModelType == typeof(Template))
            {
                cb.AttachedModel = cb.JsonPayload["template"].ToObject<Template>();
            }

            return cb;
        }

        public HelloSignCallbackEvent Event { get; internal set; }

        protected Type ExpectedAttachedModelType
        {
            get
            {
                switch (Event.EventType)
                {
                    case EventNames.AccountConfirmed:
                        return typeof(Account);
                    case EventNames.SignatureRequestViewed:
                    case EventNames.SignatureRequestSigned:
                    case EventNames.SignatureRequestSent:
                    case EventNames.SignatureRequestRemind:
                    case EventNames.SignatureRequestAllSigned:
                    case EventNames.FileError:
                    case EventNames.UnknownError:
                    case EventNames.SignatureRequestInvalid:
                        return typeof(SignatureRequest);
                    case EventNames.TemplateCreated:
                    case EventNames.TemplateError:
                        return typeof(Template);
                    default:
                        return null;
                }
            }
        }

        public object AttachedModel { get; internal set; }

        public T AttachedModelAs<T>()
            where T : class
        {
            return AttachedModel as T;
        }
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
        [Obsolete("Use EventMetadata.ReportedForAccountId")]
        public string AccountGuid { get; set; }
    }

    public class HelloSignCallbackEventMetadata
    {
        [JsonProperty("reported_for_account_id")]
        public string ReportedForAccountId { get; set; }
        [JsonProperty("related_signature_id")]
        public string RelatedSignatureId { get; set; }
        [JsonProperty("reported_for_app_id")]
        public string ReportedForAppId { get; set; }
    }
}
