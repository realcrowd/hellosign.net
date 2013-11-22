﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    internal class EmbeddedWrapper
    {
        [JsonProperty("embedded")]
        public Embedded Embedded { get; internal set; }
    }

    public class Embedded
    {
        [JsonProperty("sign_url")]
        public string SignUrl { get; internal set; }

        [JsonProperty("expires_at")]
        public long ExpiresAt { get; internal set; }
    }

    public class EmbeddedGetSignUrlRequest : IHelloSignRequest
    {
        public string SignatureId { get; set; }

        public IDictionary<string, object> ToRequestParams()
        {
            return new Dictionary<string, object>()
            {
                { "signature_id", SignatureId }
            };
        }
    }
}
