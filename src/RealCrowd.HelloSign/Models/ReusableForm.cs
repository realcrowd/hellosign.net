// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RealCrowd.HelloSign.Models
{
    [Obsolete]
    internal class ReusableFormWrapper
    {
        [JsonProperty("reusable_form")]
        public ReusableForm ReusableForm { get; set; }
    }

    [Obsolete]
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

    [Obsolete]
    public class ReusableFormList
    {
        [JsonProperty("list_info")]
        public ListInfo ListInfo { get; internal set; }
        [JsonProperty("reusable_forms")]
        public IList<ReusableForm> ReusableForms { get; internal set; }
    }

    [Obsolete]
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

    [Obsolete]
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

    [Obsolete]
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

    [Obsolete]
    [Serializable]
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
