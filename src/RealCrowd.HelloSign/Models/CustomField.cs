// Copyright (c) RealCrowd, Inc. All rights reserved. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCrowd.HelloSign.Models
{
    public class CustomField
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }
        [JsonProperty("type")]
        public string Type { get; internal set; }
    }
}
