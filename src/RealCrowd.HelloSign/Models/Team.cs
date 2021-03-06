﻿// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RealCrowd.HelloSign.Models
{
    internal class TeamWrapper
    {
        [JsonProperty("team")]
        public Team Team { get; internal set; }
    }

    [Serializable]
    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("accounts")]
        public IList<AccountCondensedWithRole> Accounts { get; internal set; }
        [JsonProperty("invited_accounts")]
        public IList<AccountCondensed> InvitedAccounts { get; internal set; }
    }

    [Serializable]
    public class TeamCreateRequest : IHelloSignRequest
    {
        public string Name { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
            {
                { "name", Name }
            };
        }
    }

    [Serializable]
    public class TeamUpdateRequest : IHelloSignRequest
    {
        public string Name { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
            {
                { "name", Name }
            };
        }
    }

    [Serializable]
    public class TeamAddMemberRequest : IHelloSignRequest
    {
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(AccountId))
                data.Add("account_id", AccountId);
            else if (!string.IsNullOrEmpty(EmailAddress))
                data.Add("email_address", EmailAddress);
            return data;
        }
    }

    [Serializable]
    public class TeamRemoveMemberRequest : IHelloSignRequest
    {
        public string AccountId { get; set; }
        public string EmailAddress { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(AccountId))
                data.Add("account_id", AccountId);
            else if (!string.IsNullOrEmpty(EmailAddress))
                data.Add("email_address", EmailAddress);
            return data;
        }
    }
}
