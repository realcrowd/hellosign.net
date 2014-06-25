// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace RealCrowd.HelloSign
{
    public class Settings : ISettings
    {
        private HelloSignSettings settings;

        public Settings()
        {
            LoadSettings();
        }

        public HelloSignSettings HelloSignSettings
        {
            get { return settings; }
        }

        private void LoadSettings()
        {
            settings = new HelloSignSettings
            {
                BaseUrl = "https://api.hellosign.com/v3",
                Endpoints = new Endpoints
                {
                    Account = new AccountEndpoints
                    {
                        Create = new Endpoint { Method = "POST", Url = "/account/create" },
                        Get = new Endpoint { Method = "GET", Url = "/account" },
                        Update = new Endpoint { Method = "POST", Url = "/account" }
                    },
                    Embedded = new EmbeddedEndpoints
                    {
                        GetSignUrl = new Endpoint { Method = "GET", Url = "/embedded/sign_url/{signature_id}" }
                    },
                    ReusableForm = new ReusableFormEndpoints
                    {
                        AddUser = new Endpoint { Method = "POST", Url = "/reusable_form/add_user/{reusable_form_id}" },
                        Get = new Endpoint { Method = "GET", Url = "/reusable_form/{reusable_form_id}" },
                        List = new Endpoint { Method = "GET", Url = "/reusable_form/list" },
                        RemoveUser = new Endpoint { Method = "POST", Url = "/reusable_form/remove_user/{reusable_form_id}" }
                    },
                    SignatureRequest = new SignatureRequestEndpoints
                    {
                        Cancel = new Endpoint { Method = "POST", Url = "/signature_request/cancel/{signature_request_id}" },
                        CreateEmbedded = new Endpoint { Method = "POST", Url = "/signature_request/create_embedded" },
                        CreateEmbeddedWithReusableForm = new Endpoint { Method = "POST", Url = "/signature_request/create_embedded_with_reusable_form" },
                        CreateEmbeddedWithTemplate = new Endpoint { Method = "POST", Url = "/signature_request/create_embedded_with_template" },
                        Get = new Endpoint { Method = "GET", Url = "/signature_request/{signature_request_id}" },
                        GetFinalCopy = new Endpoint { Method = "GET", Url = "/signature_request/final_copy/{signature_request_id}", ContentType = "application/pdf" },
                        List = new Endpoint { Method = "GET", Url = "/signature_request/list" },
                        Remind = new Endpoint { Method = "POST", Url = "/signature_request/remind/{signature_request_id}" },
                        Send = new Endpoint { Method = "POST", Url = "/signature_request/send" },
                        SendFormWithTemplate = new Endpoint { Method = "POST", Url = "/signature_request/send_with_template" },
                        SendForm = new Endpoint { Method = "POST", Url = "/signature_request/send_with_reusable_form" }
                    },
                    Team = new TeamEndpoints
                    {
                        AddMember = new Endpoint { Method = "POST", Url = "/team/add_member" },
                        Create = new Endpoint { Method = "POST", Url = "/team/create" },
                        Destory = new Endpoint { Method = "POST", Url = "/team/destroy" },
                        Get = new Endpoint { Method = "GET", Url = "/team" },
                        RemoveMember = new Endpoint { Method = "POST", Url = "/team/remove_member" },
                        Update = new Endpoint { Method = "POST", Url = "/team" }
                    },
                    Template = new TemplateEndpoints
                    {
                        List = new Endpoint(){Method ="GET", Url="/template/list"},
                        Get = new Endpoint(){Method = "GET", Url = "/template/{template_id}"}
                    },
                    UnclaimedDraft = new UnclaimedDraftEndpoints
                    {
                        Create = new Endpoint { Method = "POST", Url = "/unclaimed_draft/create" }
                    }
                }
            };
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
        [JsonProperty("embedded")]
        public EmbeddedEndpoints Embedded { get; set; }
        [JsonProperty("template")]
        public TemplateEndpoints Template { get; set; }
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
        [JsonProperty("sendFormWithTemplate")]
        public Endpoint SendFormWithTemplate { get; set; }
        [JsonProperty("createEmbeddedWithReusableForm")]
        public Endpoint CreateEmbeddedWithReusableForm { get; set; }
        [JsonProperty("createEmbedded")]
        public Endpoint CreateEmbedded { get; set; }
        [JsonProperty("createEmbeddedWithTemplate")]
        public Endpoint CreateEmbeddedWithTemplate { get; set; }
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
    public class TemplateEndpoints
    {
        [JsonProperty("list")]
        public Endpoint List { get; set; }
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
    }

    public class UnclaimedDraftEndpoints
    {
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
    }

    public class EmbeddedEndpoints
    {
        [JsonProperty("getSignUrl")]
        public Endpoint GetSignUrl { get; set; }
    }

    public class Endpoint
    {
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
