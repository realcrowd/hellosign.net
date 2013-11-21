using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class AccountWrapper
    {
        [JsonProperty("account")]
        public Account Account { get; set; }
    }

    public class Account : AccountCondensedWithRole
    {
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; internal set; }
    }

    public class AccountCondensedWithRole : AccountCondensed
    {
        [JsonProperty("role_code")]
        public string RoleCode { get; internal set; }
    }

    public class AccountCondensed
    {
        [JsonProperty("account_id")]
        public string AccountId { get; internal set; }
        [JsonProperty("email_address")]
        public string EmailAddress { get; internal set; }
    }

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
}
