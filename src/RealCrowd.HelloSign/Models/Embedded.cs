using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class EmbeddedWrapper
    {
        [JsonProperty("embedded")]
        public Embedded Embedded { get; internal set; }
    }

    [Serializable]
    public class Embedded
    {
        [JsonProperty("sign_url")]
        public string SignUrl { get; internal set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; internal set; }
    }

    [Serializable]
    public class EmbeddedGetSignUrlRequest : IHelloSignRequest
    {
        public string SignatureId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>()
            {
                { "signature_id", SignatureId }
            };
        }
    }

    [Serializable]
    public class EmbeddedGetEditUrlRequest : IHelloSignRequest
    {
        public string TemplateId { get; set; }
        public bool SkipSignerRoles { get; set; }
        public bool SkipSubjectAndMessage { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>()
            {
                { "template_id", TemplateId },
                { "skip_signer_roles", SkipSignerRoles },
                { "skip_subject_message", SkipSubjectAndMessage }
            };
        }
    }
}
