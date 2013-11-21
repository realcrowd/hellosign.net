// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign
{
    public class Settings : ISettings
    {
        private HelloSignSettings settings;

        public Settings(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string text = reader.ReadToEnd();
                settings = JsonConvert.DeserializeObject<HelloSignSettings>(text);
            }
        }

        public HelloSignSettings HelloSignSettings
        {
            get { return settings; }
        }
    }

    public class HelloSignSettings
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("endpoints")]
        public Endpoints Endpoints { get; set; }
    }

    public class Endpoints
    {
        [JsonProperty("account")]
        public AccountEndpoints Account { get; set; }
        [JsonProperty("reusableForm")]
        public ReusableFormEndpoints ReusableForm { get; set; }
        [JsonProperty("signatureRequest")]
        public SignatureRequestEndpoints SignatureRequest { get; set; }
        [JsonProperty("team")]
        public TeamEndpoints Team { get; set; }
        [JsonProperty("unclaimedDraft")]
        public UnclaimedDraftEndpoints UnclaimedDraft { get; set; }
    }

    public class AccountEndpoints
    {
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("update")]
        public Endpoint Update { get; set; }
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
    }

    public class ReusableFormEndpoints
    {
        [JsonProperty("list")]
        public Endpoint List { get; set; }
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("addUser")]
        public Endpoint AddUser { get; set; }
        [JsonProperty("removeUser")]
        public Endpoint RemoveUser { get; set; }
    }

    public class SignatureRequestEndpoints
    {
        [JsonProperty("send")]
        public Endpoint Send { get; set; }
        [JsonProperty("list")]
        public Endpoint List { get; set; }
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("remind")]
        public Endpoint Remind { get; set; }
        [JsonProperty("getFinalCopy")]
        public Endpoint GetFinalCopy { get; set; }
        [JsonProperty("cancel")]
        public Endpoint Cancel { get; set; }
        [JsonProperty("sendForm")]
        public Endpoint SendForm { get; set; }
    }

    public class TeamEndpoints
    {
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
        [JsonProperty("update")]
        public Endpoint Update { get; set; }
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("destroy")]
        public Endpoint Destory { get; set; }
        [JsonProperty("addMember")]
        public Endpoint AddMember { get; set; }
        [JsonProperty("removeMember")]
        public Endpoint RemoveMember { get; set; }
    }

    public class UnclaimedDraftEndpoints
    {
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
    }

    public class Endpoint
    {
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
