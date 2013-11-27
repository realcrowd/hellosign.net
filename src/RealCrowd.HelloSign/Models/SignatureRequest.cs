// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class SignatureRequestWrapper
    {
        [JsonProperty("signature_request")]
        public SignatureRequest SignatureRequest { get; set; }
    }

    public class SignatureRequest
    {
        [JsonProperty("test_mode")]
        public bool TestMode { get; internal set; }
        [JsonProperty("signature_request_id")]
        public string SignatureRequestId { get; internal set; }
        [JsonProperty("requester_email_address")]
        public string RequesterEmailAddress { get; internal set; }
        [JsonProperty("title")]
        public string Title { get; internal set; }
        [JsonProperty("subject")]
        public string Subject { get; internal set; }
        [JsonProperty("message")]
        public string Message { get; internal set; }
        [JsonProperty("is_complete")]
        public string IsComplete { get; internal set; }
        [JsonProperty("has_error")]
        public bool HasError { get; internal set; }
        [JsonProperty("final_copy_uri")]
        public string FinalCopyUri { get; internal set; }
        [JsonProperty("signing_url")]
        public string SigningUrl { get; internal set; }
        [JsonProperty("signing_redirect_url")]
        public string SigningRedirectUrl { get; internal set; }
        [JsonProperty("details_url")]
        public string DetailsUrl { get; internal set; }
        [JsonProperty("cc_email_addresses")]
        public IList<string> CcEmailAddresses { get; internal set; }
        [JsonProperty("custom_fields")]
        public IList<CustomField> CustomFields { get; internal set; }
        [JsonProperty("response_data")]
        public IList<ResponseData> ResponseData { get; internal set; }
        [JsonProperty("signatures")]
        public IList<Signature> Signatures { get; internal set; }
    }

    public class ResponseData
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("value")]
        public string Value { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
    }

    public class Signature
    {
        [JsonProperty("signature_id")]
        public string SignatureId { get; internal set; }
        [JsonProperty("signer_email_address")]
        public string SignerEmailAddress { get; internal set; }
        [JsonProperty("signer_name")]
        public string SignerName { get; internal set; }
        [JsonProperty("order")]
        public string Order { get; internal set; }
        [JsonProperty("status_code")]
        public string StatusCode { get; internal set; }
        [JsonProperty("signed_at")]
        public string SignedAt { get; internal set; }
        [JsonProperty("last_viewed_at")]
        public string LastViewedAt { get; internal set; }
        [JsonProperty("last_reminded_at")]
        public string LastRemindedAt { get; internal set; }
    }

    public class SignatureRequestList
    {
        [JsonProperty("list_info")]
        public ListInfo ListInfo { get; internal set; }
        [JsonProperty("signature_requests")]
        public IList<SignatureRequest> SignatureRequests { get; internal set; }
    }

    public class SignatureRequestGetRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }
        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "signature_request_id", SignatureRequestId }
                };
        }
    }

    public class SignatureRequestListRequest : IHelloSignRequest
    {
        public int? Page { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = null;
            if (Page.HasValue)
                data = new Dictionary<string, object> { { "page", Page } };
            return data;
        }
    }

    public class SignatureRequestSendRequest : IHelloSignRequest
    {
        public int? TestMode { get; set; }
        public IList<string> Files { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IList<SignerRequest> Signers { get; set; }
        public IList<string> CcEmailAddresses { get; set; }
        public IList<IList<FormFieldsRequest>> FormFieldsPerDocument { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            if (TestMode.HasValue)
                data.Add("test_mode", TestMode.Value);

            for (int i = 0; i < Files.Count; i++)
            {
                data.Add("file[" + i + "]", Files[i]);
            }

            if (!string.IsNullOrEmpty(Title))
                data.Add("title", Title);

            if (!string.IsNullOrEmpty(Subject))
                data.Add("subject", Subject);

            if (!string.IsNullOrEmpty(Message))
                data.Add("message", Message);

            for (int i = 0; i < Signers.Count; i++)
            {
                data.Add("signers[" + i + "][name]", Signers[i].Name);
                data.Add("signers[" + i + "][email_address]", Signers[i].EmailAddress);
                if (Signers[i].Order.HasValue)
                    data.Add("signers[" + i + "][order]", Signers[i].Order.Value);
            }

            if (CcEmailAddresses != null)
            {
                for (int i = 0; i < CcEmailAddresses.Count; i++)
                {
                    data.Add("cc_email_addresses[" + i + "]", CcEmailAddresses[i]);
                }
            }

            if (FormFieldsPerDocument != null && FormFieldsPerDocument.Count > 0)
            {
                string json = JsonConvert.SerializeObject(FormFieldsPerDocument);
                data.Add("form_fields_per_document", json);
            }

            return data;
        }
    }

    public class SignerRequest
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int? Order { get; set; }
    }

    public class FormFieldsRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("required")]
        public bool Required { get; set; }
        [JsonProperty("signer")]
        public int Signer { get; set; }
    }

    public class SignatureRequestSendReusableFormRequest : IHelloSignRequest
    {
        public int? TestMode { get; set; }
        public string ReusableFormId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IDictionary<string, SignatureRequestSignerRequest> Signers { get; set; }
        public IDictionary<string, string> Ccs { get; set; }
        public IDictionary<string, string> CustomFields { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            if (TestMode.HasValue)
                data.Add("test_mode", TestMode.Value);

            data.Add("reusable_form_id", ReusableFormId);

            if (!string.IsNullOrEmpty(Title))
                data.Add("title", Title);

            if (!string.IsNullOrEmpty(Subject))
                data.Add("subject", Subject);

            if (!string.IsNullOrEmpty(Message))
                data.Add("message", Message);

            foreach (KeyValuePair<string, SignatureRequestSignerRequest> signer in Signers)
            {
                data.Add("signers[" + signer.Key + "][name]", signer.Value.Name);
                data.Add("signers[" + signer.Key + "][email_address]", signer.Value.EmailAddress);
            }

            if (Ccs != null)
            {
                foreach (KeyValuePair<string, string> cc in Ccs)
                {
                    data.Add("ccs[" + cc.Key + "][email_address]", cc.Value);
                }
            }

            if (CustomFields != null)
            {
                foreach (KeyValuePair<string, string> customField in CustomFields)
                {
                    data.Add("custom_fields[" + customField.Key + "]", customField.Value);
                }
            }

            return data;
        }
    }

    public class SignatureRequestSignerRequest
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }

    public class SignatureRequestSendReusableFormCcRequest
    {
        public string EmailAddress { get; set; }
    }

    public class SignatureRequestRemindRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }
        public string EmailAddress { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "signature_request_id", SignatureRequestId },
                    { "email_address", EmailAddress }
                };
        }
    }

    public class SignatureRequestCancelRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "signature_request_id", SignatureRequestId }
                };
        }
    }

    public class SignatureRequestFinalCopyRequest : IHelloSignRequest
    {
        public string SignatureRequestId { get; set; }
        
        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>
                {
                    { "signature_request_id", SignatureRequestId }
                };
        }
    }
}
