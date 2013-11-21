using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class ReusableFormWrapper
    {
        [JsonProperty("reusable_form")]
        public ReusableForm ReusableForm { get; set; }
    }

    public class ReusableForm
    {
        [JsonProperty("reusable_form_id")]
        public string ReusableFormId { get; internal set; }
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
    }

    public class SignerRole
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("order")]
        public int? Order { get; internal set; }
    }

    public class CcRole
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
    }

    public class Document
    {
        [JsonProperty("form_fields")]
        public IList<FormField> FormFields { get; internal set; }
        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; internal set; }
    }

    public class FormField
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
        [JsonProperty("x")]
        public double X { get; internal set; }
        [JsonProperty("y")]
        public double Y { get; internal set; }
        [JsonProperty("width")]
        public int Width { get; internal set; }
        [JsonProperty("height")]
        public int Height { get; internal set; }
        [JsonProperty("required")]
        public bool Required { get; internal set; }
    }

    public class ReusableFormList
    {
        [JsonProperty("list_info")]
        public ListInfo ListInfo { get; internal set; }
        [JsonProperty("reusable_forms")]
        public IList<ReusableForm> ReusableForms { get; internal set; }
    }

    public class ReusableFormGetRequest : IHelloSignRequest
    {
        public string ReusableFormId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "reusable_form_id", ReusableFormId }
                };
        }
    }

    public class ReusableFormListRequest : IHelloSignRequest
    {
        public int? Page { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = null;
            if (Page.HasValue)
            {
                data = new Dictionary<string, object>
                {
                    { "page", Page.Value }
                };
            }
            return data;
        }
    }

    public class ReusableFormAddUserRequest : IHelloSignRequest
    {
        public string ReusableFormId { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "reusable_form_id", ReusableFormId }
            };

            if (!string.IsNullOrEmpty(AccountId))
                data.Add("account_id", AccountId);
            else if (!string.IsNullOrEmpty(EmailAddress))
                data.Add("email_address", EmailAddress);

            return data;
        }
    }

    public class ReusableFormRemoveUserRequest : IHelloSignRequest
    {
        public string ReusableFormId { get; set; }
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }
        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "reusable_form_id", ReusableFormId }
            };

            if (!string.IsNullOrEmpty(AccountId))
                data.Add("account_id", AccountId);
            else if (!string.IsNullOrEmpty(EmailAddress))
                data.Add("email_address", EmailAddress);

            return data;
        }
    }
}
