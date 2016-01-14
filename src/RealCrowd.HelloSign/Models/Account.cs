// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RealCrowd.HelloSign.Models
{
    internal class AccountWrapper
    {
        [JsonProperty("account")]
        public Account Account { get; set; }
    }

    [Serializable]
    public class Account : AccountCondensedWithRole
    {
        /// <summary>
        /// The URL that HelloSign events will be POSTed to.
        /// </summary>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; internal set; }

        /// <summary>
        /// Returns true if the user has a paid HelloSign account.
        /// </summary>
        [JsonProperty("is_paid_hs")]
        public bool IsPaidHelloSignAccount { get; internal set; }

        /// <summary>
        /// Returns true f the user has a paid HelloFax account.
        /// </summary>
        [JsonProperty("is_paid_hf")]
        public bool IsPaidHelloFaxAccount { get; internal set; }

        /// <summary>
        /// An object detailing remaining monthly quotas.
        /// </summary>
        [JsonProperty("quotas")]
        public AccountQuotas Quotas { get; internal set; }
    }

    [Serializable]
    public class AccountCondensedWithRole : AccountCondensed
    {
        /// <summary>
        /// The membership role for the team. a = Admin, m = Member.
        /// </summary>
        [JsonProperty("role_code")]
        public string RoleCode { get; internal set; }
    }

    [Serializable]
    public class AccountCondensed
    {
        /// <summary>
        /// The id of the Account.
        /// </summary>
        [JsonProperty("account_id")]
        public string AccountId { get; internal set; }
        /// <summary>
        /// The email address associated with the Account.
        /// </summary>
        [JsonProperty("email_address")]
        public string EmailAddress { get; internal set; }
    }

    [Serializable]
    public class AccountQuotas
    {
        /// <summary>
        /// API templates remaining.
        /// </summary>
        [JsonProperty("templates_left")]
        public int? ApiTemplatesLeft { get; internal set; }
        /// <summary>
        /// API signature requests remaining.
        /// </summary>
        [JsonProperty("api_signature_requests_left")]
        public int? ApiSignatureRequestsLeft { get; internal set; }
        /// <summary>
        /// Signature requests remaining.
        /// </summary>
        [JsonProperty("documents_left")]
        public int? DocumentsLeft { get; internal set; }
    }

    [Serializable]
    public class AccountUpdateRequest : IHelloSignRequest
    {
        public string CallbackUrl { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object> 
                { 
                    { "callback_url", CallbackUrl } 
                };
        }
    }

    [Serializable]
    public class AccountCreateRequest : IHelloSignRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "email_address", EmailAddress },
                    { "password", Password }
                };
        }
    }

    [Serializable]
    public class AccountVerifyRequest : IHelloSignRequest
    {
        public string EmailAddress { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "email_address", EmailAddress }
                };
        }
    }
}
