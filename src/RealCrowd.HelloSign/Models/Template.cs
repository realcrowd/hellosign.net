using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RealCrowd.HelloSign.Models
{
    [Serializable]
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

        [JsonProperty("accounts")]
        public IList<AccountCondensed> Accounts { get; internal set; }

        [JsonProperty("is_creator")]
        public bool IsCreator { get; internal set; }

        [JsonProperty("can_edit")]
        public bool CanEdit { get; internal set; }
    }

    [Serializable]
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

    [Serializable]
    public class TemplateListResponse
    {
        [JsonProperty("list_info")]
        public ListInfo ListInfo { get; set; }

        [JsonProperty("templates")]
        public IList<Template> Templates { get; set; } 
    }

    [Serializable]
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

    [Serializable]
    public class TemplateResponse
    {
        [JsonProperty("template")]
        public Template Template { get; internal set; }
    }

    [Serializable]
    public class SignerRole
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("order")]
        public int? Order { get; internal set; }
    }

    [Serializable]
    public class CcRole
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
    }

    [Serializable]
    public class Document
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("index")]
        public int? Index { get; internal set; }
        [JsonProperty("form_fields")]
        public IList<FormField> FormFields { get; internal set; }
        [JsonProperty("custom_fields")]
        public IList<CustomFieldTemplate> CustomFields { get; internal set; }
    }

    [Serializable]
    public class FormField
    {
        [JsonProperty("api_id")]
        public string ApiId { get; internal set; }
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("x")]
        public double X { get; internal set; }
        [JsonProperty("y")]
        public double Y { get; internal set; }
        [JsonProperty("width")]
        public double Width { get; internal set; }
        [JsonProperty("height")]
        public double Height { get; internal set; }
        [JsonProperty("required")]
        public bool Required { get; internal set; }
    }
}
