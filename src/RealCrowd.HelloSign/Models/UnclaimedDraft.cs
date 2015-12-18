// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class UnclaimedDraftWrapper
    {
        [JsonProperty("unclaimed_draft")]
        public UnclaimedDraft UnclaimedDraft { get; internal set; }
    }

    public class UnclaimedDraft
    {
        [JsonProperty("claim_url")]
        public string ClaimUrl { get; internal set; }
        [JsonProperty("signing_redirect_url")]
        public string SigningRedirectUrl { get; internal set; }
        [JsonProperty("expires_at")]
        public long ExpiresAt { get; internal set; }
        [JsonProperty("test_mode")]
        public string TestMode { get; internal set; }
    }

    public class UnclaimedDraftCreateRequest : IHelloSignRequest
    {
        public int? TestMode { get; set; }
        public IList<string> Files { get; set; }
        public string Type { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public IList<SignerRequest> Signers { get; set; }
        public IList<string> CcEmailAddresses { get; set; }
        public string RequesterEmailAddress { get; set; }
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

            if (!string.IsNullOrEmpty(Type))
                data.Add("type", Type);

            if (!string.IsNullOrEmpty(Subject))
                data.Add("subject", Subject);

            if (!string.IsNullOrEmpty(Message))
                data.Add("message", Message);

            if (Signers != null)
            {
                for (int i = 0; i < Signers.Count; i++)
                {
                    data.Add("signers[" + i + "][name]", Signers[i].Name);
                    data.Add("signers[" + i + "][email_address]", Signers[i].EmailAddress);
                    if (Signers[i].Order.HasValue)
                        data.Add("signers[" + i + "][order]", Signers[i].Order.Value);
                }
            }

            if (CcEmailAddresses != null)
            {
                for (int i = 0; i < CcEmailAddresses.Count; i++)
                {
                    data.Add("cc_email_addresses[" + i + "]", CcEmailAddresses[i]);
                }
            }

            if (!string.IsNullOrEmpty(RequesterEmailAddress))
                data.Add("requester_email_address", RequesterEmailAddress);

            if (FormFieldsPerDocument != null && FormFieldsPerDocument.Count > 0)
            {
                string json = JsonConvert.SerializeObject(FormFieldsPerDocument);
                data.Add("form_fields_per_document", json);
            }

            return data;
        }
    }
}
