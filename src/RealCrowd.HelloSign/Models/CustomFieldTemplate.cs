// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace RealCrowd.HelloSign.Models
{
    public class CustomFieldTemplate
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
    }
}
