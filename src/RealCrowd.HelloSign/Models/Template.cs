using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RealCrowd.HelloSign.Models
{
    public class Template
    {
        [JsonProperty("template_id")]
        public string TemplateId { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        [JsonProperty("message")]
        public string Message { get; internal set; }

        [JsonProperty("signer_roles")]
        public IList<SignerRole> SignerRoles { get; internal set; }

        [JsonProperty("cc_roles")]
        public IList<CcRole> CcRoles { get; internal set; }

        [JsonProperty("documents")]
        public IList<Document> Documents { get; internal set; }
    }

    public class TemplateListRequest : IHelloSignRequest
    {
        [JsonProperty("page")]
        public string Page { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            var data = new Dictionary<string, object>();

            if(!string.IsNullOrEmpty(Page))
                data.Add("page",Page);

            return data;
        }
    }

    public class TemplateListResponse
    {
        [JsonProperty("list_info")]
        public ListInfo ListInfo { get; set; }

        [JsonProperty("templates")]
        public IList<Template> Templates { get; set; } 
    }
    public class TemplateRequest : IHelloSignRequest
    {
        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            var data = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(TemplateId))
                data.Add("template_id", TemplateId);

            return data;
        }
    }
    public class TemplateResponse
    {
        [JsonProperty("template")]
        public Template Template { get; internal set; }
    }
}
