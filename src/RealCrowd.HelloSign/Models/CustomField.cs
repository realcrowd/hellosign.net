using Newtonsoft.Json;
using System;

namespace RealCrowd.HelloSign.Models
{
    [Serializable]
    public class CustomField : CustomFieldTemplate
    {
        [JsonProperty("value")]
        public object Value { get; internal set; }
    }
}
