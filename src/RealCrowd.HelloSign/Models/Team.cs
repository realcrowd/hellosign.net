using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class TeamWrapper
    {
        [JsonProperty("team")]
        public Team Team { get; internal set; }
    }

    public class Team
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("accounts")]
        public IList<AccountCondensedWithRole> Accounts { get; internal set; }
        [JsonProperty("invited_accounts")]
        public IList<AccountCondensed> InvitedAccounts { get; internal set; }
    }

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
