// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;

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
                        Update = new Endpoint { Method = "POST", Url = "/account" },
                        Verify = new Endpoint { Method = "POST", Url = "/account/verify" }
                    },
                    Embedded = new EmbeddedEndpoints
                    {
                        GetSignUrl = new Endpoint { Method = "GET", Url = "/embedded/sign_url/{signature_id}" },
                        GetTemplateEditUrl = new Endpoint { Method = "GET", Url = "/embedded/edit_url/{signature_id}" }
                    },
                    SignatureRequest = new SignatureRequestEndpoints
                    {
                        Cancel = new Endpoint { Method = "POST", Url = "/signature_request/cancel/{signature_request_id}" },
                        Send = new Endpoint { Method = "POST", Url = "/signature_request/send" },
                        SendWithTemplate = new Endpoint { Method = "POST", Url = "/signature_request/send_with_template" },
                        SendEmbedded = new Endpoint { Method = "POST", Url = "/signature_request/create_embedded" },
                        SendEmbeddedWithTemplate = new Endpoint { Method = "POST", Url = "/signature_request/create_embedded_with_template" },
                        Get = new Endpoint { Method = "GET", Url = "/signature_request/{signature_request_id}" },
                        GetFiles = new Endpoint { Method = "GET", Url = "/signature_request/files/{signature_request_id}" },
                        List = new Endpoint { Method = "GET", Url = "/signature_request/list" },
                        Remind = new Endpoint { Method = "POST", Url = "/signature_request/remind/{signature_request_id}" },
                        CreateEmbeddedWithReusableForm = new Endpoint { Method = "POST", Url = "/signature_request/create_embedded_with_reusable_form" },
                        SendForm = new Endpoint { Method = "POST", Url = "/signature_request/send_with_reusable_form" },
                        GetFinalCopy = new Endpoint { Method = "GET", Url = "/signature_request/final_copy/{signature_request_id}", ContentType = "application/pdf" },
                    },
                    Team = new TeamEndpoints
                    {
                        AddMember = new Endpoint { Method = "POST", Url = "/team/add_member" },
                        Create = new Endpoint { Method = "POST", Url = "/team/create" },
                        Delete = new Endpoint { Method = "POST", Url = "/team/destroy" },
                        Get = new Endpoint { Method = "GET", Url = "/team" },
                        RemoveMember = new Endpoint { Method = "POST", Url = "/team/remove_member" },
                        Update = new Endpoint { Method = "POST", Url = "/team" }
                    },
                    Template = new TemplateEndpoints
                    {
                        List = new Endpoint(){Method ="GET", Url="/template/list"},
                        Get = new Endpoint(){Method = "GET", Url = "/template/{template_id}"},
                        CreateEmbeddedDraft = new Endpoint() { Method = "GET", Url = "/template/create_embedded_draft" },
                        Delete = new Endpoint() { Method = "POST", Url = "/template/delete/{template_id}" },
                        AddUser = new Endpoint() { Method = "POST", Url = "/template/add_user/{template_id}" },
                        RemoveUser = new Endpoint() { Method = "POST", Url = "/template/remove_user/{template_id}" },
                        GetFiles = new Endpoint() { Method = "GET", Url = "/template/files/{template_id}", ContentType = "application/pdf" }
                    },
                    UnclaimedDraft = new UnclaimedDraftEndpoints
                    {
                        Create = new Endpoint { Method = "POST", Url = "/unclaimed_draft/create" },
                        CreateEmbedded = new Endpoint {  Method = "POST", Url = "/unclaimed_draft/create_embedded" },
                        CreateEmbeddedWithTemplate = new Endpoint { Method = "POST", Url = "/unclaimed_draft/create_embedded_with_template" }
                    },
                    ApiApp = new ApiAppEndpoints
                    {
                        Create = new Endpoint { Method = "POST", Url = "/api_app" },
                        Get = new Endpoint { Method = "GET", Url = "/api_app/{client_id}" },
                        List = new Endpoint { Method = "GET", Url = "/api_app/list" },
                        Update = new Endpoint { Method = "POST", Url = "/api_app/{client_id}" },
                        Delete = new Endpoint { Method = "DELTE", Url = "/api_app/{client_id}" }
                    },
                    ReusableForm = new ReusableFormEndpoints
                    {
                        AddUser = new Endpoint { Method = "POST", Url = "/reusable_form/add_user/{reusable_form_id}" },
                        Get = new Endpoint { Method = "GET", Url = "/reusable_form/{reusable_form_id}" },
                        List = new Endpoint { Method = "GET", Url = "/reusable_form/list" },
                        RemoveUser = new Endpoint { Method = "POST", Url = "/reusable_form/remove_user/{reusable_form_id}" }
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
        [JsonProperty("apiApp")]
        public ApiAppEndpoints ApiApp { get; set; }
        [Obsolete("Use Template")]
        [JsonProperty("reusableForm")]
        public ReusableFormEndpoints ReusableForm { get; set; }
    }

    public class AccountEndpoints
    {
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("update")]
        public Endpoint Update { get; set; }
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
        [JsonProperty("verify")]
        public Endpoint Verify { get; set; }
    }

    public class SignatureRequestEndpoints
    {
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("list")]
        public Endpoint List { get; set; }
        [JsonProperty("send")]
        public Endpoint Send { get; set; }
        [JsonProperty("sendWithTemplate")]
        public Endpoint SendWithTemplate { get; set; }
        [JsonProperty("remind")]
        public Endpoint Remind { get; set; }
        [JsonProperty("cancel")]
        public Endpoint Cancel { get; set; }
        [JsonProperty("getFiles")]
        public Endpoint GetFiles { get; set; }
        [JsonProperty("sendEmbedded")]
        public Endpoint SendEmbedded { get; set; }
        [JsonProperty("sendEmbeddedWithTemplate")]
        public Endpoint SendEmbeddedWithTemplate { get; set; }

        [Obsolete("Use SendWithTemplate")]
        [JsonProperty("sendForm")]
        public Endpoint SendForm { get; set; }
        [Obsolete("Use SendEmbeddedWithTemplate")]
        [JsonProperty("createEmbeddedWithReusableForm")]
        public Endpoint CreateEmbeddedWithReusableForm { get; set; }
        [Obsolete("Use GetFiles")]
        [JsonProperty("getFinalCopy")]
        public Endpoint GetFinalCopy { get; set; }
    }

    public class TeamEndpoints
    {
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
        [JsonProperty("update")]
        public Endpoint Update { get; set; }
        [JsonProperty("delete")]
        public Endpoint Delete { get; set; }
        [JsonProperty("addMember")]
        public Endpoint AddMember { get; set; }
        [JsonProperty("removeMember")]
        public Endpoint RemoveMember { get; set; }
    }
    public class TemplateEndpoints
    {
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("list")]
        public Endpoint List { get; set; }
        [JsonProperty("addUser")]
        public Endpoint AddUser { get; set; }
        [JsonProperty("removeUser")]
        public Endpoint RemoveUser { get; set; }
        [JsonProperty("createEmbeddedDraft")]
        public Endpoint CreateEmbeddedDraft { get; set; }
        [JsonProperty("delete")]
        public Endpoint Delete { get; set; }
        [JsonProperty("getFiles")]
        public Endpoint GetFiles { get; set; }
    }

    public class UnclaimedDraftEndpoints
    {
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
        [JsonProperty("createEmbedded")]
        public Endpoint CreateEmbedded { get; set; }
        [JsonProperty("createEmbeddedWithTemplate")]
        public Endpoint CreateEmbeddedWithTemplate { get; set; }
    }

    public class EmbeddedEndpoints
    {
        [JsonProperty("getSignUrl")]
        public Endpoint GetSignUrl { get; set; }
        [JsonProperty("getTemplateEditUrl")]
        public Endpoint GetTemplateEditUrl { get; set; }
    }

    public class ApiAppEndpoints
    {
        [JsonProperty("get")]
        public Endpoint Get { get; set; }
        [JsonProperty("list")]
        public Endpoint List { get; set; }
        [JsonProperty("create")]
        public Endpoint Create { get; set; }
        [JsonProperty("update")]
        public Endpoint Update { get; set; }
        [JsonProperty("delete")]
        public Endpoint Delete { get; set; }
    }

    [Obsolete("User TemplateEndpoints")]
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
